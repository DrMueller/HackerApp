using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameRoundRow
    {
        [Parameter]
        [EditorRequired]
        public required GameRound GameRound { get; set; }

        [Parameter]
        [EditorRequired]
        public EventCallback<IReadOnlyCollection<string>> OnShowErorrRequested { get; set; }

        private async Task ShowErrorsAsync()
        {
            var warnings = GameRound.Validate();

            await OnShowErorrRequested.InvokeAsync(warnings);
        }
    }
}