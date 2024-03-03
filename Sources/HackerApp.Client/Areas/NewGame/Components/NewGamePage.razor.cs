using HackerApp.Client.Areas.RunningGame.Components;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components;

public partial class NewGamePage
{
    public const string Path = "/games/new";

    [Inject]
    public IGameState GameState { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; }

    private IList<Player> Players { get; } = new List<Player>();

    private void HandleAddPlayerClicked()
    {
        Players.Add(new Player());
    }

    private async Task HandleStartGameClicked()
    {
        await GameState.InitializeAsync(Players.ToList());
        Navigator.NavigateTo(RunningGamePage.Path);
    }
}