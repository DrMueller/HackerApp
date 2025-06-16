using HackerApp.Client.Infrastructure.InformationHandling.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Infrastructure.InformationHandling.Components
{
    public partial class Informations
    {
        [Parameter]
        public InformationEntries? Entries { get; set; }

        private bool HasErrors => Entries?.HasErrors ?? false;

        private bool HasInfos => Entries?.InfoMessages.Any() ?? false;

        private bool HasWarnings => Entries?.WarningMessages.Any() ?? false;
    }
}