using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherApp.Shared.Services;
using WeatherApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the WeatherApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
