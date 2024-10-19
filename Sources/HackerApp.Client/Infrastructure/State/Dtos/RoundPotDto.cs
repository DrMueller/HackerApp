using HackerApp.Client.Areas.Shared.Models;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class RoundPotDto
    {
        public double Value { get; init; }

        public static RoundPotDto MapFromModel(RoundPot model)
        {
            return new RoundPotDto
            {
                Value = model.Value
            };
        }
    }
}