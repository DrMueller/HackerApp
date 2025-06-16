using Blazored.LocalStorage;
using HackerApp.Client.Areas.Login;
using HackerApp.Components;
using HackerApp.Infrastructure.ExceptionHandling;
using HackerApp.Infrastructure.SemKer.Services;
using HackerApp.Infrastructure.SemKer.Services.Implementation;
using HackerApp.Infrastructure.Settings.Config.Services;
using HackerApp.Infrastructure.Settings.Provisioning.Models;
using HackerApp.Infrastructure.Settings.Provisioning.Services;
using HackerApp.Infrastructure.Settings.Provisioning.Services.Implementation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using _Imports = HackerApp.Client._Imports;

namespace HackerApp
{
    [UsedImplicitly]
    public class Program
    {
        private static IConfiguration Configuration { get; } = ConfigurationFactory.Create(typeof(Program).Assembly);

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents()
                .AddAuthenticationStateSerialization();

            builder.Services.AddBlazorBootstrap();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IGameAnalyzer, GameAnalyzer>();
            builder.Services.AddSingleton<ISettingsProvider, SettingsProvider>();
            builder.Services.AddAuthorization();

            var appSettingsSection = Configuration.GetSection(AppSettings.SectionKey);
            builder.Services.Configure<AppSettings>(appSettingsSection);

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = LoginPage.Path;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(720);
                options.SlidingExpiration = true;
            });

            var app = builder.Build();
            app.UseGlobalExceptionHandler();

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
            app.MapControllers();
            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(_Imports).Assembly);

            app.Run();
        }
    }
}