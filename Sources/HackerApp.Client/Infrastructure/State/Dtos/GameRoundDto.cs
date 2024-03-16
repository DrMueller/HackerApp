using HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class GameRoundDto
    {
        public required List<PlayerGameRoundDto> PlayerGameRounds { get; set; }

        public double RoundEinsatz { get; set; }
    }
}