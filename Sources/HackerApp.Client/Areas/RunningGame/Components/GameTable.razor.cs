using BlazorBootstrap;
using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameTable
    {
        private Modal modal = default!;

        [Parameter]
        public required Game Game { get; set; }

        [Parameter]
        public required EventCallback<PlayerPenalty> OnPenaltyAdded { get; set; }

        private async Task ShowPlayerPenalty(string playerName)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add(nameof(PlayerPenaltyEdit.PlayerName), playerName);
            parameters.Add(nameof(PlayerPenaltyEdit.OnNewPenalty),
                EventCallback.Factory.Create<PlayerPenalty>(this, async pen => { await OnPenaltyAdded.InvokeAsync(pen); }));

            await modal.ShowAsync<PlayerPenalty>("Busse", parameters: parameters);
        }
    }
}