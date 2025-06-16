using HackerApp.Client.Infrastructure.Invariance;

namespace HackerApp.Client.Infrastructure.InformationHandling.Models
{
    public class InformationEntry
    {
        public InformationEntry(InformationType type, string message)
        {
            Guard.StringNotNullOrEmpty(() => message);

            Type = type;
            Message = message;
        }

        public string Message { get; }
        public InformationType Type { get; }
    }
}