namespace HackerApp.Client.Areas.Shared.Models
{
    public class PlayerLossProfitCalculation(Player player, double overallLossProfit)
    {
        public bool CanReceive => OverallLossProfit > 0;
        public bool HasToPay => OverallLossProfit < 0;

        public double OverallLossProfit { get; private set; } = overallLossProfit;
        public Player Player { get; } = player;

        public void Add(double value)
        {
            OverallLossProfit += value;
        }

        public void Subtract(double value)
        {
            OverallLossProfit -= value;
        }
    }
}