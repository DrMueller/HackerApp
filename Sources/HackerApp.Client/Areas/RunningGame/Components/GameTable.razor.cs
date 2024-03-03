using HackerApp.Client.Areas.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class GameTable
    {
        [Parameter]
        public required Game Game { get; set; }
    }
}
