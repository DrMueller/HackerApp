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
                var potFromPenalties = PlayerGameRounds.Sum(f => f.RoundPenalty);
                var roundPlayerEinsatz = RoundEinsatz * PlayerGameRounds.Count;
                if (prevRound == null)
                {
                    return new RoundPot(potFromPenalties + roundPlayerEinsatz);
                }

                var loosers = prevRound.PlayerGameRounds.Where(f => f.Result.ResultType is GameRoundPlayerResultType.MitgegangenVerloren or GameRoundPlayerResultType.HackedVerloren).ToList();

                if (loosers.Any())
                {
                    var potValue = loosers.Sum(f => prevRound.CalculcateEarnings(f.Player)) * -1;

                    return new RoundPot(potValue + potFromPenalties);
                }

                if (prevRound.PlayerGameRounds.All(f => f.Result.ResultType == GameRoundPlayerResultType.None))
                {
                    var newRoundPot = RoundEinsatz * PlayerGameRounds.Count;
                    return new RoundPot(newRoundPot + prevRound.RoundPot.Value + potFromPenalties);
                }

                // Winners, ausbezahlt
                return new RoundPot(roundPlayerEinsatz + potFromPenalties);
            }
        }

        public static GameRound Create(
            double einsatz,
            IReadOnlyCollection<Player> players,
            GameRound? prevRound,
            IReadOnlyCollection<PlayerPenalty> penalties)
        {
            var playerGameRounds = players.Select(player =>
            {
                var result = new GameRoundPlayerResult();
                var penalty = penalties.SingleOrDefault(f => player.Name == f.PlayerName);

                return new PlayerGameRound(
                    player,
                    result,
                    penalty);
            }).ToList();

            var roundNumber = prevRound?.RoundNumber + 1 ?? 0;

            return new GameRound(
                roundNumber,
                einsatz,
                playerGameRounds,
                prevRound);
        }

        public void AddPenalty(PlayerPenalty pen)
        {
            PlayerGameRounds
                .Single(f => f.Player.Name == pen.PlayerName)
                .AddPenalty(pen);
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

            return playerGameRound.CalculateLossProfit(RoundPot, RoundEinsatz, winnerCount);
        }
    }
}