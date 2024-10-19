using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds
{
    [PublicAPI]
    public class PlayerPenaltyDto
    {
        public double PenaltyValue { get; init; }
        public required string PlayerName { get; init; }

        public static PlayerPenaltyDto? MapFromModel(PlayerPenalty? model)
        {
            if (model == null)
            {
                return null;
            }

            return new PlayerPenaltyDto
            {
                PlayerName = model.PlayerName,
                PenaltyValue = model.PenaltyValue
            };
        }
    }
}