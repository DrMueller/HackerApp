using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Infrastructure.State.Dtos;

namespace HackerApp.Client.Infrastructure.State.Services
{
    public interface IGameState
    {
        Task<Game> LoadAsync();
        Task<GameDto> LoadDtoAsync();
        Task PersistAsync(Game game);
    }
}