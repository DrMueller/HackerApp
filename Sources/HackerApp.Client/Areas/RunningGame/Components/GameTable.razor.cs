using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameTable
    {
        [Parameter]
        public required Game Game { get; set; }

        [Parameter]
        public required EventCallback<PlayerPenalty> OnPenaltyAdded { get; set; }

        private PlayerPenaltyEdit PlayerPenaltyEditRef { get; set; } = null!;

        private async Task ShowPlayerPenalty(string playerName)
        {
            await PlayerPenaltyEditRef.ShowAsync(playerName);
        }
    }
}