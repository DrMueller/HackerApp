using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components;

public partial class EditPlayers
{
    [Parameter] public required IList<Player> Players { get; set; }

    private void HandleRemovePlayerButtonClicked(Player player)
    {
        Players.Remove(player);
    }
}