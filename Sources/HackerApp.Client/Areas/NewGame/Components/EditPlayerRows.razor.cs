using HackerApp.Client.Areas.NewGame.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components
{
    public partial class EditPlayerRows
    {
        [Parameter]
        public required IList<NewPlayer>? Players { get; set; }

        [Parameter]
        public required EventCallback<NewPlayer> RemovePlayerRequired { get; set; }

        private async Task RemovePlayer(NewPlayer player)
        {
            await RemovePlayerRequired.InvokeAsync(player);
        }
    }
}