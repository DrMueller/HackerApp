using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.Pgr;

namespace HackerApp.Client.UnitTests.Areas.Shared.Models.Pgr
{
    public partial class PlayerGameRoundEarningsUnitTests
    {
        private static PlayerGameRound CreatePgr(
            GameRoundPlayerResultType resType)
        {
            var player = new Player(Guid.NewGuid().ToString());
            var res = new GameRoundPlayerResult();
            var round = new PlayerGameRound(
                player,
                res,
                null);

            round.Result.ResultType = resType;

            return round;
        }
    }
}