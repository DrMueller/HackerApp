using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Infrastructure.State.Dtos;

namespace HackerApp.Client.Infrastructure.State.Services.Servants
{
    public interface IGameMapper
    {
        Game Map(GameDto dto);
        GameDto Map(Game game);
    }
}