using HackerApp.Client.Areas.NewGame.Models;
using HackerApp.Client.Areas.RunningGame.Components;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Infrastructure.State.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components
{
    [UsedImplicitly]
    public partial class NewGamePage
    {
        private const string Path = "/";

        [Inject]
        public required IGameState GameState { get; set; }

        [Inject]
        public required NavigationManager Navigator { get; set; }

        private IList<NewPlayer> Players { get; } = new List<NewPlayer>();

        private void LoadLastGame()
        {
            Navigator.NavigateTo(RunningGamePage.Path);
        }

        private async Task StartNewGame()
        {
            var players = Players.Select(f => new Player(f.Name)).ToList();

            var newGame = new Game(players);
            await GameState.PersistAsync(newGame);
            Navigator.NavigateTo(RunningGamePage.Path);
        }
    }
}