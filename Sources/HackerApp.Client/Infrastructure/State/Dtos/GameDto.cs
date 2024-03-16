using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class GameDto
    {
        public required List<GameRoundDto> GameRounds { get; set; }
        public required List<PlayerDto> Players { get; set; }
    }
}