using HackerApp.Client.Areas.Shared.Models.Pgr;
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