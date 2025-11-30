using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.Pgr;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameTable
    {
        [Parameter]
        public required Game Game { get; set; }

        [Parameter]
        public required EventCallback<PlayerPenalty> OnPenaltyAdded { get; set; }

        [Parameter]
        [EditorRequired]
        public EventCallback<IReadOnlyCollection<string>> OnShowErorrRequested { get; set; }

        private IEnumerable<GameRound> GameRounds => ItemsPerPage == -1 ? Game.GameRounds : Game.GameRounds.Take(ItemsPerPage);
        private int ItemsPerPage { get; set; } = 20;
        private PlayerPenaltyEdit PlayerPenaltyEditRef { get; set; } = null!;

        private async Task HandleOnShowErrorRequestedAsync(IReadOnlyCollection<string> arg)
        {
            await OnShowErorrRequested.InvokeAsync(arg);
        }

        private async Task ShowPlayerPenalty(string playerName)
        {
            await PlayerPenaltyEditRef.ShowAsync(playerName);
        }
    }
}