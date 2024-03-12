using Blazored.LocalStorage;
using HackerApp.Client.Areas.Shared.Models;

namespace HackerApp.Client.Areas.Shared.Services.Implementation
{
    public class GameState(ILocalStorageService localStorage) : IGameState
    {
        private const string GameKey = "GameKey";

        public async Task InitializeAsync(IReadOnlyCollection<string> playerNames)
        {
           

            await localStorage.RemoveItemAsync(GameKey);
            await localStorage.SetItemAsync(GameKey, playerNames);
        }

        public async Task<List<string>> LoadPlayerNamesAsync()
        {
            var item = await localStorage.GetItemAsync<List<string>>(GameKey);

            return item!;
        }

        public async Task SaveAsync(Game game)
        {
            //await localStorage.SetItemAsync(GameKey, game);
        }
    }
}