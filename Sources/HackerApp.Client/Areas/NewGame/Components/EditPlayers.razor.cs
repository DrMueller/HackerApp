using HackerApp.Client.Areas.NewGame.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.NewGame.Components
{
    public partial class EditPlayers
    {
        [Parameter]
        public EventCallback<bool> OnValidationChanged { get; set; }

        [Parameter]
        public required IList<NewPlayer> Players { get; set; }

        private async Task AddPlayerAsync()
        {
            Players.Add(new NewPlayer
            {
                Name = string.Empty
            });

            await ValidateAsync();
        }

        private async Task RemovePlayerAsync(NewPlayer obj)
        {
            Players.Remove(obj);
            await ValidateAsync();
        }

        private async Task ValidateAsync()
        {
            var validation = Players.Any() &&
                             Players.All(f => !string.IsNullOrEmpty(f.Name))
                             &&
                             Players.Select(f => f.Name).Distinct().Count() == Players.Count;

            await OnValidationChanged.InvokeAsync(validation);
        }
    }
}