using HackerApp.Client.Areas.Shared.Models.Pgr;

namespace HackerApp.Client.Areas.Shared.Models
{
    public class Game(
        IReadOnlyCollection<Player> players,
        IList<GameRound> rounds)
    {
        public Game(IReadOnlyCollection<Player> players)
            : this(players, new List<GameRound>())
        {
        }

        public GameRound CurrentRound => rounds[0];
        public IReadOnlyCollection<GameRound> GameRounds => rounds.AsReadOnly();
        public IReadOnlyCollection<Player> Players { get; } = players;

        public void AddNewRound(
            double roundEinsatz,
            IReadOnlyCollection<PlayerPenalty> penalties)
        {
            rounds.Insert(0, GameRound.Create(
                roundEinsatz,
                Players,
                rounds.FirstOrDefault(),
                penalties));
        }

        public IReadOnlyCollection<PlayerPayment> CalculatePayouts()
        {
            var calc = new List<PlayerLossProfitCalculation>();

            foreach (var player in Players)
            {
                var playerLossOverallProfit = player.CalculateLossProfit(GameRounds, LossProfitType.All);

                calc.Add(new PlayerLossProfitCalculation(player, playerLossOverallProfit));
            }

            var calcs = new PlayerLossProfitCalculations(calc);

            return calcs.CalculatePayments();
        }

        public void DeleteLastRound()
        {
            if (!rounds.Any())
            {
                return;
            }

            rounds.RemoveAt(0);
        }
    }
}