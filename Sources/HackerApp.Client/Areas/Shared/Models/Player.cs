namespace HackerApp.Client.Areas.Shared.Models
{
    public class Player(string name)
    {
        public string Name { get; } = name;

        public double CalculateOverallLossProfit(IReadOnlyCollection<GameRound> rounds)
        {
            // Skip the first, as it wasnt played out
            var roundsToCount = rounds.Skip(1).ToList();

            var einsaetze = roundsToCount
                .Where(f => f.EinsatzWasPaid)
                .Where(f => f.PlayerGameRounds.Any(g => g.Player.Name == Name))
                .Sum(f => f.RoundEinsatz) * -1;

            var earnings = roundsToCount.Sum(f => f.CalculcateEarnings(this));

            return einsaetze + earnings;
        }
    }
}