using HackerApp.Client.Areas.NewGame.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components
{
    public partial class EditPlayers
    {
        [Parameter]
        public required IList<NewPlayer> Players { get; set; }

        private void HandleAddPlayerClicked()
        {
            Players.Add(new NewPlayer
            {
                Name = string.Empty
            });
        }

        private void RemovePlayer(NewPlayer obj)
        {
            Players.Remove(obj);
        }
    }
}