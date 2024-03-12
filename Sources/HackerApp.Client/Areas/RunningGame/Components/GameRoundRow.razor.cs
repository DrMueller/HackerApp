using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameRoundRow
    {
        [Parameter]
        public required GameRound GameRound { get; set; }
    }
}
