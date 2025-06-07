using BlazorBootstrap;
using HackerApp.Client.Areas.Shared.Models;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class PlayerPayouts
    {
        private Modal ModalRef { get; set; } = null!;

        private IReadOnlyCollection<PlayerPayment> Payouts { get; set; } = null!;

        public async Task ShowAsync(IReadOnlyCollection<PlayerPayment> payouts)
        {
            Payouts = payouts;
            await ModalRef.ShowAsync();
        }

        private async Task CancelAsync()
        {
            await ModalRef.HideAsync();
        }
    }
}