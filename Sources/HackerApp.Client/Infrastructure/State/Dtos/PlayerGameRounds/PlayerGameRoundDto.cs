using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds
{
    [PublicAPI]
    public class PlayerGameRoundDto
    {
        public required PlayerDto Player { get; set; }
        public required GameRoundPlayerResultDto Result { get; set; }
    }
}