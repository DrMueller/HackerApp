using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class GameRoundDto
    {
        public required int RoundNumber { get; init; }

        public required List<PlayerGameRoundDto> PlayerGameRounds { get; init; }

        public double RoundEinsatz { get; init; }
    
        public static GameRoundDto MapFromModel(GameRound model)
        {
            return new GameRoundDto
            {
                PlayerGameRounds = model.PlayerGameRounds.Select(PlayerGameRoundDto.MapFromModel).ToList(),
                RoundNumber = model.RoundNumber,
                RoundEinsatz = model.RoundEinsatz
            };
        }
    }
}