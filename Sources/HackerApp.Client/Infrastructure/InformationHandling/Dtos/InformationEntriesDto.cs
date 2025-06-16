using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.InformationHandling.Dtos
{
    [PublicAPI]
    public class InformationEntriesDto
    {
        public List<string> ErrorMessages { get; init; } = [];
        public List<string> InfoMessages { get; init; } = [];
        public List<string> WarningMessages { get; init; } = [];
    }
}