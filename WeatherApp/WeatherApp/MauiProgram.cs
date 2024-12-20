using Microsoft.Extensions.Logging;
using WeatherApp.Services;
using WeatherApp.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "weather.db");
            builder.Services.AddDbContext<WeatherContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped<IWeatherDataService, WeatherDataService>();

            // Add device-specific services used by the WeatherApp.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<IWeatherService, WeatherService>();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Ensure the database is created
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
                dbContext.EnsureDatabaseCreated();
            }

            return app;
        }
    }
}
