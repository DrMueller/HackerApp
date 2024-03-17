using System.Globalization;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;

namespace HackerApp.Client.Areas.Shared.Models
{
    public class GameRound(
        int roundNumber,
        double roundEinsatz,
        IReadOnlyCollection<PlayerGameRound> playerGameRounds,
        GameRound? prevRound)
    {
        public bool EinsatzWasPaid => PlayerGameRounds.All(x => x.Result.ResultType != GameRoundPlayerResultType.MitgegangenVerloren && x.Result.ResultType != GameRoundPlayerResultType.HackedVerloren);
        public IReadOnlyCollection<PlayerGameRound> PlayerGameRounds { get; } = playerGameRounds;

        public double RoundEinsatz { get; } = roundEinsatz;

        public string RoundEinsatzDescription
        {
            get
            {
                var result = RoundEinsatz.ToString(CultureInfo.InvariantCulture);
                if (EinsatzWasPaid)
                {
                    result += " *";
                }

                return result;
            }
        }

        public int RoundNumber { get; } = roundNumber;

        public RoundPot RoundPot
        {
            get
            {
                if (prevRound == null)
                {
                    return new RoundPot(RoundEinsatz * PlayerGameRounds.Count);
                }

                var loosers = prevRound.PlayerGameRounds.Where(f => f.Result.ResultType is GameRoundPlayerResultType.MitgegangenVerloren or GameRoundPlayerResultType.HackedVerloren).ToList();

                if (loosers.Any())
                {
                    var potValue = loosers.Sum(f => prevRound.CalculcateEarnings(f.Player)) * -1;

                    return new RoundPot(potValue);
                }

                if (prevRound.PlayerGameRounds.All(f => f.Result.ResultType == GameRoundPlayerResultType.None))
                {
                    var newRoundPot = RoundEinsatz * PlayerGameRounds.Count;
                    return new RoundPot(newRoundPot + prevRound.RoundPot.Value);
                }

                // Winners, ausbezahlt
                return new RoundPot(RoundEinsatz * PlayerGameRounds.Count);
            }
        }

        public static GameRound Create(
            double einsatz,
            IReadOnlyCollection<Player> players,
            GameRound? prevRound)
        {
            var playerGameRounds = players.Select(f => new PlayerGameRound(f, new GameRoundPlayerResult())).ToList();

            var roundNumber = prevRound?.RoundNumber + 1 ?? 0;

            return new GameRound(
                roundNumber,
                einsatz,
                playerGameRounds,
                prevRound);
        }

        public double CalculcateEarnings(Player player)
        {
            var playerGameRound = PlayerGameRounds.Single(f => f.Player.Name == player.Name);

            var mitgegangenGewonnenCount = PlayerGameRounds.Count(f => f.Result.ResultType == GameRoundPlayerResultType.MitgegangenGewonnen);

            var hasHackerWinner = PlayerGameRounds.Any(f => f.Result.ResultType == GameRoundPlayerResultType.HackedGewonnen);

            var winnerCount = mitgegangenGewonnenCount;
            if (hasHackerWinner)
            {
                winnerCount += 2;
            }

            return playerGameRound.CalculateLossProfit(RoundPot, winnerCount);
        }
    }
}