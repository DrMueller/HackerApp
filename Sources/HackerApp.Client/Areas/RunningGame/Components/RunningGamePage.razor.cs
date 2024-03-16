using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Infrastructure.State.Services;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class RunningGamePage
    {
        public const string Path = "/games/run";

        [Inject]
        public required IGameState GameState { get; set; }

        [Parameter]
        public required IReadOnlyCollection<Player> Players { get; set; }

        private double Einsatz { get; set; } = 0.50;

        private Game? Game { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Game = await GameState.LoadAsync();
        }

        private void HandleNewRoundClicked()
        {
            Game!.AddNewRound(Einsatz);
            GameState.PersistAsync(Game);
        }
    }
}