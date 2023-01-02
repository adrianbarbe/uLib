using Microsoft.EntityFrameworkCore;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Mappers.Storage;
using RemoteFinder.BLL.Services.FileService;
using RemoteFinder.BLL.Services.OAuth2Service;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models.Configuration;
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

// Mappers
builder.Services.AddSingleton<IMapper<FileEntity, File>, FileMapper>();

// Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IOAuth2Service, OAuth2Service>();


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