using HackerApp.Client.Areas.NewGame.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components
{
    public partial class EditPlayerRow
    {
        [Parameter]
        public required NewPlayer Player { get; set; }

        [Parameter]
        public required EventCallback<NewPlayer> RemovePlayerRequired { get; set; }

        [Parameter]
        public required EventCallback<NewPlayer> PlayerNameChanged { get; set; }

        private async Task SetPlayerNameAsync(string? obj)
        {
            Player.Name = obj ?? string.Empty;
            await PlayerNameChanged.InvokeAsync(Player);
        }
    }
}
