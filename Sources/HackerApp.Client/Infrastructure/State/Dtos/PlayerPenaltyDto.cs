using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class PlayerPenaltyDto
    {
        public double PenaltyValue { get; init; }
        public required string PlayerName { get; init; }
    }
}