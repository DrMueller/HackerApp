using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.State.Dtos
{
    [PublicAPI]
    public class PlayerDto
    {
        public required string Name { get; set; }
    }
}