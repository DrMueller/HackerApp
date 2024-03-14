using Blazored.LocalStorage;
using HackerApp.Client.Areas.Shared.Services;
using HackerApp.Client.Areas.Shared.Services.Implementation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HackerApp.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddScoped<IGameState, GameState>();
            builder.Services.AddBlazoredLocalStorage();

            await builder.Build().RunAsync();
        }
    }
}