using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds
{
    [PublicAPI]
    public class PlayerGameRoundDto
    {
        public required PlayerDto Player { get; init; }
        public required GameRoundPlayerResultDto Result { get; init; }
        public required PlayerPenaltyDto Penalty { get; init; }
    }
}