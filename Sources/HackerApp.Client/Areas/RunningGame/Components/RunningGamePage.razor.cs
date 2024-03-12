using System.Diagnostics;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class RunningGamePage
    {
        public const string Path = "/games/run";

        private double Einsatz { get; set; } = 0.50;

        [Inject]
        public IGameState GameState { get; set; }

        [Parameter]
        public required IReadOnlyCollection<Player> Players { get; set; }

        private Game? Game { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var playerNames = await GameState.LoadPlayerNamesAsync();

            var players = playerNames.Select(f => new Player(f)).ToList();

            Game = new Game(players);
        }

        private async Task Tra()
        {
            Game.NewRound(Einsatz);
        }
    }
}