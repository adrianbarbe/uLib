using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Mappers.Storage;
using RemoteFinder.BLL.Services.AwsMinioClient;
using RemoteFinder.BLL.Services.FileService;
using RemoteFinder.BLL.Services.OAuth2Service;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models.Configuration;
using UMSA.StepTest.BLL.Configuration;
using File = RemoteFinder.Models.File;

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
            }
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

// Configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<AwsMinioSettings>(builder.Configuration.GetSection("AwsMinioSettings"));

// Mappers
builder.Services.AddSingleton<IMapper<FileEntity, File>, FileMapper>();

// Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IOAuth2Service, OAuth2Service>();
builder.Services.AddScoped<IAwsMinioClient, AswMinioClient>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(corsAllowOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();