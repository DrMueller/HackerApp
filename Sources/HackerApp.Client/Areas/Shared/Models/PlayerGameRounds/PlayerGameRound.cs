namespace HackerApp.Client.Areas.Shared.Models.PlayerGameRounds
{
    public class PlayerGameRound(
        Player player,
        GameRoundPlayerResult result,
        PlayerPenalty? penalty)
    {
        public PlayerPenalty? Penalty { get; private set; } = penalty;
        public Player Player { get; } = player;
        public GameRoundPlayerResult Result { get; } = result;
        public double RoundPenalty => Penalty?.PenaltyValue ?? 0;

        public string RoundPenaltyDescription
        {
            get
            {
                if (Penalty == null)
                {
                    return string.Empty;
                }

                return $"({Penalty.PenaltyValue})";
            }
        }

        public double CalculateLossProfit(
            RoundPot roundPot,
            int amountOfWinners)
        {
            switch (Result.ResultType)
            {
                case GameRoundPlayerResultType.None:
                    return 0;
                case GameRoundPlayerResultType.HackedVerloren:
                    return roundPot.Value * 2 * -1;
                case GameRoundPlayerResultType.MitgegangenVerloren:
                    return roundPot.Value * -1;
                case GameRoundPlayerResultType.HackedGewonnen:
                case GameRoundPlayerResultType.MitgegangenGewonnen:
                    return CalculateEarnings(roundPot, amountOfWinners);
            }

            throw new Exception("Tra");
        }

        public void AddPenalty(PlayerPenalty penalty)
        {
            Penalty = penalty;
        }

        private double CalculateEarnings(
            RoundPot roundPot,
            int amountOfWinners)
        {
            var moneyParts = roundPot.Value / amountOfWinners;

            if (Result.ResultType == GameRoundPlayerResultType.HackedGewonnen)
            {
                return moneyParts * 2;
            }

            return moneyParts;
        }
    }
}