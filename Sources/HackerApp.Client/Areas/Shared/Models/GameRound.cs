using System.Globalization;
using HackerApp.Client.Areas.Shared.Models.Pgr;

namespace HackerApp.Client.Areas.Shared.Models
{
    public class GameRound(
        int roundNumber,
        double roundEinsatz,
        PlayerGameRounds playerGameRounds,
        GameRound? prevRound)
    {
        public PlayerGameRounds PlayerGameRounds { get; } = playerGameRounds;

        public double RoundEinsatz { get; } = roundEinsatz;

        public string RoundEinsatzDescription
        {
            get
            {
                var result = RoundEinsatz.ToString(CultureInfo.InvariantCulture);
                if (PlayerGameRounds.EinsatzWasPaid)
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
                var potFromPenalties = PlayerGameRounds.Rounds.Sum(f => f.RoundPenalty);
                var roundPlayerEinsatz = RoundEinsatz * PlayerGameRounds.Rounds.Count;
                if (prevRound == null)
                {
                    return new RoundPot(potFromPenalties + roundPlayerEinsatz);
                }

                var loosers = prevRound.PlayerGameRounds.Rounds.Where(f => f.Result.ResultType is GameRoundPlayerResultType.MitgegangenVerloren or GameRoundPlayerResultType.HackedVerloren).ToList();

                if (loosers.Any())
                {
                    var potValue = loosers.Sum(f => prevRound.CalculcateEarnings(f.Player)) * -1;

                    return new RoundPot(potValue + potFromPenalties);
                }

                if (prevRound.PlayerGameRounds.Rounds.All(f => f.Result.ResultType == GameRoundPlayerResultType.None))
                {
                    var newRoundPot = RoundEinsatz * PlayerGameRounds.Rounds.Count;
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
                new PlayerGameRounds(playerGameRounds),
                prevRound);
        }

        public void AddPenalty(PlayerPenalty pen)
        {
            PlayerGameRounds
                .Rounds
                .Single(f => f.Player.Name == pen.PlayerName)
                .AddPenalty(pen);
        }

        public double CalculcateEarnings(Player player)
        {
            var earnings = new PlayerGameRoundEarnings(RoundPot, PlayerGameRounds, player);

            return earnings.CalculateLossProfit();
        }
    }
}