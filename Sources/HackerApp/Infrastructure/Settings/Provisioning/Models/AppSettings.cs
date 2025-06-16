using JetBrains.Annotations;

namespace HackerApp.Infrastructure.Settings.Provisioning.Models
{
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public class AppSettings
    {
        public const string SectionKey = "AppSettings";
        public required string UserName { get; set; }
        public required string UserPassword { get; set; }
        public required string OpenAiEndpoint { get; set; }
        public required string OpenAiKey { get; set; }
    }
}