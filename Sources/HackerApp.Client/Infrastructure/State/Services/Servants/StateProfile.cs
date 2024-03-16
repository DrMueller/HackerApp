using AutoMapper;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using HackerApp.Client.Infrastructure.State.Dtos;
using HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds;

namespace HackerApp.Client.Infrastructure.State.Services.Servants
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Player, PlayerDto>();
            CreateMap<GameRound, GameRoundDto>();
            CreateMap<RoundPot, RoundPotDto>();
            CreateMap<PlayerGameRound, PlayerGameRoundDto>();
            CreateMap<GameRoundPlayerResult, GameRoundPlayerResultDto>();
        }
    }
}