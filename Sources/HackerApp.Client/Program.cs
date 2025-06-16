using Blazored.LocalStorage;
using HackerApp.Client.Infrastructure.HttpRequests;
using HackerApp.Client.Infrastructure.HttpRequests.Implementation;
using HackerApp.Client.Infrastructure.HttpRequests.Servants;
using HackerApp.Client.Infrastructure.HttpRequests.Servants.Implementation;
using HackerApp.Client.Infrastructure.JavaScript.Services;
using HackerApp.Client.Infrastructure.JavaScript.Services.Implementation;
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
            builder.Services.AddSingleton<IGameMapper, GameMapper>();

            builder.Services.AddHttpClient(HttpCallHandler.HttpClientName);
            builder.Services.AddSingleton<IJavaScriptLocator, JavaScriptLocator>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddAuthenticationStateDeserialization();

            builder.Services.AddScoped<IHttpCallHandler, HttpCallHandler>();
            builder.Services.AddScoped<IHttpClientProxy, HttpClientProxy>();

            await builder.Build().RunAsync();
        }
    }
}