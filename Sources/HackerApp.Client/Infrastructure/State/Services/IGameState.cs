using HackerApp.Client.Areas.Shared.Models;

namespace HackerApp.Client.Infrastructure.State.Services
{
    public interface IGameState
    {
        Task<Game> LoadAsync();
        Task PersistAsync(Game game);
    }
}