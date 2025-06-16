using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Infrastructure.InformationHandling.Components
{
    public partial class InfoList
    {
        [Parameter]
        public IReadOnlyCollection<string> InfoEntries { get; set; } = null!;
    }
}