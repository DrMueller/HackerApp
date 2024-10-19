using HackerApp.Client.Areas.Shared.Models;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class GameDto
    {
        public required List<GameRoundDto> GameRounds { get; init; }
        public required List<PlayerDto> Players { get; init; }

        public static GameDto MapFromModel(Game model)
        {
            return new GameDto
            {
                GameRounds = model.GameRounds.Select(GameRoundDto.MapFromModel).ToList(),
                Players = model.Players.Select(PlayerDto.MapFromModel).ToList()
            };
        }
    }
}