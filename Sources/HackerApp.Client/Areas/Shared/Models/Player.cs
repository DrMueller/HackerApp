namespace HackerApp.Client.Areas.Shared.Models
{
    public class Player(string name)
    {
        public string Name { get; } = name;

        public double CalculateOverallLossProfit(IReadOnlyCollection<GameRound> rounds)
        {
            var einsaetze = rounds
                .Where(f => f.EinsatzWasPaid)
                .Where(f => f.PlayerGameRounds.Any(g => g.Player.Name == Name))
                .Sum(f => f.RoundEinsatz) * -1;

            var earnings = rounds.Sum(f => f.CalculcateEarnings(this));

            return einsaetze + earnings;
        }
    }
}