using BlazorBootstrap;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class PlayerPenaltyEdit
    {
        private string _playerName = default!;

        [Parameter]
        public required EventCallback<PlayerPenalty> OnNewPenalty { get; set; }

        private bool ApplyPenaltyNextRound { get; set; }
        private Modal ModalRef { get; set; } = default!;

        private double PenaltyValue { get; set; }

        public async Task ShowAsync(string playerName)
        {
            _playerName = playerName;
            await ModalRef.ShowAsync();
        }

        private async Task CancelAsync()
        {
            await ModalRef.HideAsync();
        }

        private async Task SavePenaltyAsync()
        {
            await OnNewPenalty.InvokeAsync(
                new PlayerPenalty(
                    _playerName,
                    PenaltyValue,
                    ApplyPenaltyNextRound));

            await ModalRef.HideAsync();
        }
    }
}