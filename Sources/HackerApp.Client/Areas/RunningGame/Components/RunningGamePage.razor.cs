using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components;

public partial class RunningGamePage
{
    public const string Path = "/games/run";

    private double Einsatz { get; set; } = 0.50;
    
    [Inject]
    required public IGameState GameState { get; set; }

    private Game? Game { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Game = await GameState.LoadAsync();
        await base.OnInitializedAsync();
    }

    private async Task HandleEinsatzClicked()
    {
        Game.AddSaetzu(Einsatz);
        await GameState.SaveAsync(Game);
    }

    private void HandleResultat()
    {
        
    }
}