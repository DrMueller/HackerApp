using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.HttpRequests.Dtos
{
    [PublicAPI]
    public record ServerErrorDto(string Message, string TypeName, string Stacktrace);
}