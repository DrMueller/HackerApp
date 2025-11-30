using BlazorBootstrap;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class InvalidGameRoundDescription
    {
        private IReadOnlyCollection<string> Description { get; set; } = null!;

        private Modal ModalRef { get; set; } = null!;

        public async Task ShowAsync(IReadOnlyCollection<string> description)
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