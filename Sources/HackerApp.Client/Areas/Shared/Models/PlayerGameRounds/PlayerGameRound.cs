using HackerApp.Client.Infrastructure;

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

        public void AddPenalty(PlayerPenalty penalty)
        {
            Penalty = penalty;
        }

        public double CalculateLossProfit(
            RoundPot roundPot,
            double roundEinsatz,
            int amountOfWinners)
        {
            switch (Result.ResultType)
            {
                case GameRoundPlayerResultType.None:
                    return 0;
                case GameRoundPlayerResultType.HackedVerloren:
                    return (roundPot.Value * 2 * -1).RoundToNext50Rappen();
                case GameRoundPlayerResultType.MitgegangenVerloren:
                    return (roundPot.Value * -1).RoundToNext50Rappen();
                case GameRoundPlayerResultType.HackedGewonnen:
                case GameRoundPlayerResultType.MitgegangenGewonnen:
                    return CalculateEarnings(roundPot, roundEinsatz, amountOfWinners);
            }

            throw new Exception("Tra");
        }

        private double CalculateEarnings(
            RoundPot roundPot,
            double roundEinsatz,
            int amountOfWinners)
        {
            if (Result.ResultType == GameRoundPlayerResultType.HackedGewonnen)
            {
                if (amountOfWinners == 1)
                {
                    return roundPot.Value.RoundToNext50Rappen();
                }

                var twoThirdsOfPot = roundPot.Value / 3 * 2;

                // In minpots abrunden
                var requiredMinPot = roundEinsatz * (amountOfWinners - 1);
                if (twoThirdsOfPot > requiredMinPot)
                {
                    return roundPot.Value - requiredMinPot.RoundToNext50Rappen();
                }

                return twoThirdsOfPot.RoundToNext50Rappen();
            }

            var amountMitgegangenGewonnen = amountOfWinners - 1;

            var thirdOfPot = roundPot.Value / 3;

            var relativeAmount = thirdOfPot / amountMitgegangenGewonnen;
            return relativeAmount.RoundToNext50Rappen();
        }
    }
}