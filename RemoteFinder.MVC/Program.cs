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

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();