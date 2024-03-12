using HackerApp.Client.Areas.NewGame.Models;
using HackerApp.Client.Areas.RunningGame.Components;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components
{
    public partial class NewGamePage
    {
        public const string Path = "/";

        [Inject]
        public NavigationManager Navigator { get; set; }

        [Inject]
        public IGameState GameState { get; set; }

        private IList<NewPlayer> Players { get; } = new List<NewPlayer>();

        private void HandleAddPlayerClicked()
        {
            Players.Add(new NewPlayer());
        }

        private async Task HandleStartGameClicked()
        {
            var playerNames = Players.Select(f => f.Name).ToList();
            await GameState.InitializeAsync(playerNames);
            Navigator.NavigateTo(RunningGamePage.Path);
        }
    }
}