using HackerApp.Client.Areas.NewGame.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components
{
    public partial class EditPlayers
    {
        [Parameter]
        public required IList<NewPlayer> Players { get; set; }

        private void HandleRemovePlayerButtonClicked(NewPlayer player)
        {
            Players.Remove(player);
        }
    }
}