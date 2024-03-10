using Blazored.LocalStorage;
using HackerApp.Client.Areas.Shared.Models;

namespace HackerApp.Client.Areas.Shared.Services.Implementation
{
    public class GameState(ILocalStorageService localStorage) : IGameState
    {
        private const string GameKey = "GameKey";

        public async Task InitializeAsync(IReadOnlyCollection<Player> players)
        {
            var game = new Game
            {
                Players = players.ToList(),
                Rounds = new List<GameRound>
                {
                    new()
                    {
                        //FinalPot = players.Count,
                        Results = players.Select(f =>
                            new GameRoundPlayerResult
                            {
                                ResultType = GameRoundPlayerResultType.None
                            }).ToList()
                    }
                }
            };

            await localStorage.RemoveItemAsync(GameKey);
            await localStorage.SetItemAsync(GameKey, game);
        }

        public async Task<Game> LoadAsync()
        {
            var item = await localStorage.GetItemAsync<Game>(GameKey);

            return item!;
        }

        public async Task SaveAsync(Game game)
        {
            await localStorage.SetItemAsync(GameKey, game);
        }
    }
}