using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using RemoteFinder.BLL.Authorization;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Mappers.Storage;
using RemoteFinder.BLL.Services.AuthorizationService;
using RemoteFinder.BLL.Services.AwsMinioClient;
using RemoteFinder.BLL.Services.BookService;
using RemoteFinder.BLL.Services.CategoryService;
using RemoteFinder.BLL.Services.FileService;
using RemoteFinder.BLL.Services.OAuth2Service;
using RemoteFinder.BLL.Validators;
using RemoteFinder.DAL;
using RemoteFinder.DAL.Helpers;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;
using RemoteFinder.Models.Configuration;
using RemoteFinder.Models.Constants;
using RemoteFinder.Web;
using UMSA.StepTest.BLL.Configuration;
using IAuthorizationService = RemoteFinder.BLL.Services.AuthorizationService.IAuthorizationService;

var builder = WebApplication.CreateBuilder(args);

var corsAllowOrigins = "corsAllowOrigins";

builder.Services.AddCors(options =>
{
    var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();
    var origins = corsSettings.AllowedOrigins.Split(";");

    options.AddPolicy(name: corsAllowOrigins,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                // .AllowAnyOrigin()
                .WithOrigins(origins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition", "Content-Length");
        });
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
    .AddEnvironmentVariables();


JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var expiredException = context.Exception is SecurityTokenExpiredException;

                if (expiredException)
                {
                    context.Response.Headers.Add(HeaderNames.Expires, context.Properties.ExpiresUtc.ToString());
                }

                return Task.CompletedTask;
            },
        };
        o.SaveToken = true;
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "sub",
            RoleClaimType = "role",
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(Policies.User, policy => policy.Requirements.Add(new RoleRequirement("user")));
});

builder.Services.AddDbContext<MainContext>(options =>
    {
        var connectionString = Environment.GetEnvironmentVariable("DbConnectionString") ??
                               builder.Configuration.GetSection("DbConnectionString").Get<string>();

        if (connectionString == string.Empty)
        {
            throw new Exception("No DbConnectionString ENV available");
        }

        options.UseNpgsql(connectionString);
    }
);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, RoleHandler>();


// Configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<AwsMinioSettings>(builder.Configuration.GetSection("AwsMinioSettings"));

// Mappers
builder.Services.AddSingleton<IMapper<FileEntity, FileStorage>, FileMapper>();
builder.Services.AddSingleton<IMapper<CategoryEntity, Category>, CategoryMapper>();
builder.Services.AddSingleton<IMapper<BookEntity, BookBase>, BookMapper>();

// Services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IAuthorizationDAHelper, AuthorizationDAHelper>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IOAuth2Service, OAuth2Service>();
builder.Services.AddScoped<IAwsMinioClient, AswMinioClient>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBookService, BookService>();

// Validators
builder.Services.AddTransient<IValidator<Category>, CategoryValidator>();
builder.Services.AddTransient<IValidator<BookBase>, BookBaseValidator>();
builder.Services.AddTransient<IValidator<FileStorage>, FileValidator>();

builder.Services.AddControllers(options => { options.Filters.Add(typeof(ExceptionHandlingFilter)); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(corsAllowOrigins);

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();