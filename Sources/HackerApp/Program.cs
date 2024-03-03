using Blazored.LocalStorage;
using HackerApp.Client.Areas.Shared.Services.Implementation;
using HackerApp.Client.Areas.Shared.Services;
using HackerApp.Components;
using _Imports = HackerApp.Client._Imports;

namespace HackerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IGameState, GameState>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(_Imports).Assembly);

            app.Run();
        }
    }
}