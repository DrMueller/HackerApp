using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class ResultTypeInput
    {
        [Parameter]
        public GameRoundPlayerResultType ResultType { get; set; }

        [Parameter]
        public EventCallback<GameRoundPlayerResultType> ResultTypeChanged { get; set; }
    }
}