namespace HackerApp.Client.Areas.Shared.Models
{
    public class PlayerLossProfitCalculations
    {
        private readonly IReadOnlyCollection<PlayerLossProfitCalculation> _calcs;

        public PlayerLossProfitCalculations(IReadOnlyCollection<PlayerLossProfitCalculation> calcs)
        {
            _calcs = calcs.OrderBy(f => f.OverallLossProfit).ToList();
        }

        public IReadOnlyCollection<PlayerPayment> CalculatePayments()
        {
            var payments = new List<PlayerPayment>();

            var peopleToPay = _calcs.Where(f => f.HasToPay).ToList();

            foreach (var payer in peopleToPay)
            {
                while (payer.HasToPay && _calcs.Any(f => f.CanReceive))
                {
                    var highestReceiver = _calcs.OrderByDescending(f => f.OverallLossProfit).First();
                    if (highestReceiver.OverallLossProfit - payer.OverallLossProfit * -1 > 0)
                    {
                        var absoluteVal = Math.Abs(payer.OverallLossProfit);
                        payments.Add(new PlayerPayment(payer.Player, highestReceiver.Player, absoluteVal));
                        highestReceiver.Subtract(absoluteVal);
                        payer.Add(absoluteVal);
                    }
                    else
                    {
                        var absoluteVal = Math.Abs(highestReceiver.OverallLossProfit);
                        payments.Add(new PlayerPayment(payer.Player, highestReceiver.Player, absoluteVal));
                        highestReceiver.Subtract(absoluteVal);
                        payer.Add(absoluteVal);
                    }
                }
            }

            return payments;
        }
    }
}