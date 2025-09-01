namespace HackerApp.Client.Areas.Shared.Models
{
    public enum LossProfitType
    {
        All,
        GameWins,
        Penalties
    }

    public class Player(string name)
    {
        public string Name { get; } = name;

        public double CalculateLossProfit(IReadOnlyCollection<GameRound> rounds, LossProfitType lossProfitType)
        {
            // Skip the first, as it wasnt played out
            var roundsToCount = rounds.Skip(1).ToList();

            var einsaetze = roundsToCount
                .Where(f => f.PlayerGameRounds.EinsatzWasPaid)
                .Where(f => f.PlayerGameRounds.Rounds.Any(g => g.Player.Name == Name))
                .Sum(f => f.RoundEinsatz) * -1;

            var earnings = roundsToCount.Sum(f => f.CalculcateEarnings(this));

            var penalties = rounds
                .SelectMany(f => f.PlayerGameRounds.Rounds)
                .Where(f => f.Player.Name == Name)
                .Sum(f => f.RoundPenalty) * -1;

            if (lossProfitType == LossProfitType.GameWins)
            {
                return einsaetze + earnings;
            }

            if (lossProfitType == LossProfitType.Penalties)
            {
                return penalties;
            }

            return einsaetze + earnings + penalties;
        }
    }
}