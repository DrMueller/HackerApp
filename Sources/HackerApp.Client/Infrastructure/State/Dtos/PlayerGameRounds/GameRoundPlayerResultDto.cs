using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds
{
    [PublicAPI]
    public class GameRoundPlayerResultDto
    {
        public GameRoundPlayerResultType ResultType { get; init; }
    }
}