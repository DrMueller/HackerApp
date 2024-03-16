using AutoMapper;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using HackerApp.Client.Infrastructure.State.Dtos;
using HackerApp.Client.Infrastructure.State.Dtos.PlayerGameRounds;

namespace HackerApp.Client.Infrastructure.State.Services.Servants.Implementation
{
    public class GameMapper(IMapper mapper) : IGameMapper
    {
        public Game Map(GameDto dto)
        {
            var players = dto.Players.Select(f => new Player(f.Name)).ToList();

            var rounds = new List<GameRound>();
            foreach (var roundDto in dto.GameRounds)
            {
                var round = MapGameRound(
                    roundDto,
                    players,
                    rounds.FirstOrDefault());

                rounds.Insert(0, round);
            }

            return new Game(players, rounds);
        }

        public GameDto Map(Game game)
        {
            return mapper.Map<GameDto>(game);
        }

        private static GameRound MapGameRound(GameRoundDto dto, IReadOnlyCollection<Player> players, GameRound? prevRound)
        {
            return new GameRound(
                dto.RoundEinsatz,
                dto.PlayerGameRounds.Select(f => MapPlayerGameRound(f, players)).ToList(),
                prevRound);
        }

        private static PlayerGameRound MapPlayerGameRound(PlayerGameRoundDto dto, IReadOnlyCollection<Player> players)
        {
            var player = players.Single(f => f.Name == dto.Player.Name);

            var res = new GameRoundPlayerResult
            {
                ResultType = dto.Result.ResultType
            };

            return new PlayerGameRound(player, res);
        }
    }
}