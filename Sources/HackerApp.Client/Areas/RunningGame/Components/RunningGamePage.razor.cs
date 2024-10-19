using HackerApp.Client.Areas.NewGame.Components;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using HackerApp.Client.Infrastructure.State.Services;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class RunningGamePage
    {
        public const string Path = "/games/run";

        private readonly IList<PlayerPenalty> _playerPenalties = new List<PlayerPenalty>();

        [Inject]
        public required IGameState GameState { get; set; }

        [Inject]
        public required NavigationManager Navigator { get; set; }

        [Parameter]
        public required IReadOnlyCollection<Player> Players { get; set; }

        private double Einsatz { get; set; } = 0.50;

        private Game? Game { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Game = await GameState.LoadAsync();
        }

        private async Task AddNewRoundAsync()
        {
            Game!.AddNewRound(Einsatz, _playerPenalties.ToList());
            _playerPenalties.Clear();
            await GameState.PersistAsync(Game);
        }

        private void AddPlayerPenalty(PlayerPenalty pen)
        {
            var existingPenalty = _playerPenalties.SingleOrDefault(f => f.PlayerName == pen.PlayerName);
            if (existingPenalty != null)
            {
                _playerPenalties.Remove(existingPenalty);
            }

            _playerPenalties.Add(pen);
        }

        private void CreateNewGame()
        {
            Navigator.NavigateTo(NewGamePage.Path);
        }
    }
}