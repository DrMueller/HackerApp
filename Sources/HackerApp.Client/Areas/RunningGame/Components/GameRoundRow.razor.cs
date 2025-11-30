using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameRoundRow
    {
        [Parameter]
        [EditorRequired]
        public required GameRound GameRound { get; set; }

        private InvalidGameRoundDescription InvalidGameRoundDescriptionRef { get; set; } = null!;

        private async Task ShowErrorsAsync()
        {
            var warnings = GameRound.Validate();

            await InvalidGameRoundDescriptionRef.ShowAsync(warnings);
        }
    }
}