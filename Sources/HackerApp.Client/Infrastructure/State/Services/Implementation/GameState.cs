using Blazored.LocalStorage;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Infrastructure.State.Dtos;
using HackerApp.Client.Infrastructure.State.Services.Servants;

namespace HackerApp.Client.Infrastructure.State.Services.Implementation
{
    public class GameState(ILocalStorageService localStorage, IGameMapper mapper) : IGameState
    {
        private const string GameKey = "GameKey";

        public async Task<Game> LoadAsync()
        {
            var dto = await localStorage.GetItemAsync<GameDto>(GameKey);

            return mapper.Map(dto!);
        }

        public async Task<GameDto> LoadDtoAsync()
        {
            var tra = await localStorage.GetItemAsync<GameDto>(GameKey);
            return tra!;
        }

        public async Task PersistAsync(Game game)
        {
            var dto = mapper.Map(game);

            await localStorage.RemoveItemAsync(GameKey);
            await localStorage.SetItemAsync(GameKey, dto);
        }
    }
}