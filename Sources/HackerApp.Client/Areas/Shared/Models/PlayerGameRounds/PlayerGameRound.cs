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
            int amountOfWinners)
        {
            switch (Result.ResultType)
            {
                case GameRoundPlayerResultType.None:
                    return 0;
                case GameRoundPlayerResultType.HackedVerloren:
                    return (roundPot.Value * 2 * -1).RoundTwoDigits();
                case GameRoundPlayerResultType.MitgegangenVerloren:
                    return (roundPot.Value * -1).RoundTwoDigits();
                case GameRoundPlayerResultType.HackedGewonnen:
                    return CalculateHackerEarnings(roundPot, amountOfWinners);
                case GameRoundPlayerResultType.MitgegangenGewonnen:
                    return CalculateMitgegangenEarnings(roundPot, amountOfWinners);
            }

            throw new Exception("Tra");
        }

        private static double CalculateHackerEarnings(
            RoundPot roundPot,
            int amountOfWinners)
        {
            if (amountOfWinners == 1)
            {
                return roundPot.Value.RoundTwoDigits();
            }

            var twoThirdsOfPot = roundPot.Value / 3 * 2;

            return twoThirdsOfPot.RoundTwoDigits();
        }

        private static double CalculateMitgegangenEarnings(
            RoundPot roundPot,
            int amountOfWinners)
        {
            if (amountOfWinners == 1)
            {
                return roundPot.Value.RoundTwoDigits();
            }

            var amountMitgegangenGewonnen = amountOfWinners - 1;

            var thirdOfPot = roundPot.Value / 3;

            var relativeAmount = thirdOfPot / amountMitgegangenGewonnen;
            return relativeAmount.RoundTwoDigits();
        }
    }
}