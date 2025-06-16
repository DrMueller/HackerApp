using HackerApp.Client.Infrastructure.State.Dtos;

namespace HackerApp.Infrastructure.SemKer.Services
{
    public interface IGameAnalyzer
    {
        Task<string> AnalyzeAsync(GameDto game);
    }
}
