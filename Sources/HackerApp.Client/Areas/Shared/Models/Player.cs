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

            var penalties = roundsToCount
                .SelectMany(f => f.PlayerGameRounds)
                .Where(f => f.Player.Name == Name)
                .Sum(f => f.RoundPenalty) * -1;

            return einsaetze + earnings + penalties;
        }
    }
}