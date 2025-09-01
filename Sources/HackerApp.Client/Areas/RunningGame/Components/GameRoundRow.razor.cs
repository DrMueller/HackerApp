using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameRoundRow
    {
        [Parameter]
        [EditorRequired]
        public required GameRound GameRound { get; set; }
    }
}