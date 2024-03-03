using HackerApp.Client.Areas.Shared.Models;

namespace HackerApp.Client.Areas.Shared.Services;

public interface IGameState
{
    Task InitializeAsync(IReadOnlyCollection<Player> players);

    Task<Game> LoadAsync();

    Task SaveAsync(Game game);
}