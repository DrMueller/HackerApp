using Blazored.LocalStorage;
using HackerApp.Client.Infrastructure.JavaScript.Services;
using HackerApp.Client.Infrastructure.JavaScript.Services.Implementation;
using HackerApp.Client.Infrastructure.SemKer.Services;
using HackerApp.Client.Infrastructure.SemKer.Services.Implementation;
using HackerApp.Client.Infrastructure.State.Services;
using HackerApp.Client.Infrastructure.State.Services.Implementation;
using HackerApp.Client.Infrastructure.State.Services.Servants;
using HackerApp.Client.Infrastructure.State.Services.Servants.Implementation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HackerApp.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<IGameState, GameState>();
            builder.Services.AddSingleton<IGameAnalyzer, GameAnalyzer>();
            builder.Services.AddSingleton<IGameMapper, GameMapper>();

            builder.Services.AddSingleton<IJavaScriptLocator, JavaScriptLocator>();

            await builder.Build().RunAsync();
        }
    }
}