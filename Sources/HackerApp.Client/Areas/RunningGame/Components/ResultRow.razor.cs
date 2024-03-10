using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class ResultRow
    {
        [Parameter]
        public IReadOnlyCollection<GameRoundPlayerResult> PlayerResults { get; set; }

        

    }
}
