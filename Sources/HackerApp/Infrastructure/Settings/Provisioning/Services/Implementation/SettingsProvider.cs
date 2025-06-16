using HackerApp.Infrastructure.Settings.Provisioning.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace HackerApp.Infrastructure.Settings.Provisioning.Services.Implementation
{
    [UsedImplicitly]
    internal class SettingsProvider(
        IOptions<AppSettings> appSettings)
        : ISettingsProvider
    {
        public AppSettings AppSettings => appSettings.Value;
    }
}