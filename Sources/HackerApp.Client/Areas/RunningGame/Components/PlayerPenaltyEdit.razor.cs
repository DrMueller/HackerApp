using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class PlayerPenaltyEdit
    {
        [Parameter]
        public required EventCallback<PlayerPenalty> OnNewPenalty { get; set; }

        [Parameter]
        public required string PlayerName { get; set; }

        private double PenaltyValue { get; set; }

        private async Task SavePenaltyAsync()
        {
            await OnNewPenalty.InvokeAsync(
                new PlayerPenalty(PlayerName,
                    PenaltyValue));
        }
    }
}