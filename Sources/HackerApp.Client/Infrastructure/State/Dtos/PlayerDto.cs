using HackerApp.Client.Areas.Shared.Models;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class PlayerDto
    {
        public required string Name { get; init; }
    
        public static PlayerDto MapFromModel(Player model)
        {
            return new PlayerDto
            {
                Name = model.Name
            };
        }
    }
}