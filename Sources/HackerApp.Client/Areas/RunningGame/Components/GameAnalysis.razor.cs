using BlazorBootstrap;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameAnalysis
    {
        private string? Description { get; set; }

        private Modal ModalRef { get; set; } = default!;

        public async Task ShowAsync(string description)
        {
            Description = description;
            await ModalRef.ShowAsync();
        }

        private async Task CancelAsync()
        {
            await ModalRef.HideAsync();
        }
    }
}