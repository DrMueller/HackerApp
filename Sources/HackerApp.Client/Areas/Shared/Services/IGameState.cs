using HackerApp.Client.Areas.Shared.Models;

namespace HackerApp.Client.Areas.Shared.Services;

public interface IGameState
{
    Task InitializeAsync(IReadOnlyCollection<string> playerNames);

    Task<List<string>> LoadPlayerNamesAsync();

    Task SaveAsync(Game game);
}