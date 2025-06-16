using HackerApp.Infrastructure.Settings.Provisioning.Models;

namespace HackerApp.Infrastructure.Settings.Provisioning.Services
{
    public interface ISettingsProvider
    {
        AppSettings AppSettings { get; }
    }
}