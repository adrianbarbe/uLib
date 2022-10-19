using Microsoft.EntityFrameworkCore;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Mappers.Storage;
using RemoteFinder.BLL.Services.FileService;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using File = RemoteFinder.Models.File;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<MainContext>(options =>
    {
        var connectionString = Environment.GetEnvironmentVariable("DbConnectionString") ?? "";

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


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();