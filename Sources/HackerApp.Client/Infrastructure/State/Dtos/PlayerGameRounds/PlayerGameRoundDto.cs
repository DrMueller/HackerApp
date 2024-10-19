using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds
{
    [PublicAPI]
    public class PlayerGameRoundDto
    {
        public PlayerPenaltyDto? Penalty { get; init; }
        public required PlayerDto Player { get; init; }
        public required GameRoundPlayerResultDto Result { get; init; }

        public static PlayerGameRoundDto MapFromModel(PlayerGameRound model)
        {
            return new PlayerGameRoundDto
            {
                Penalty = PlayerPenaltyDto.MapFromModel(model.Penalty),
                Player = PlayerDto.MapFromModel(model.Player),
                Result = GameRoundPlayerResultDto.MapFromModel(model.Result)
            };
        }
    }
}