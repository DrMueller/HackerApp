using HackerApp.Client.Infrastructure;

namespace HackerApp.Client.Areas.Shared.Models.Pgr
{
    public class PlayerGameRoundEarnings(
        RoundPot roundPot,
        PlayerGameRounds playerGameRounds,
        Player targetPlayer)
    {
        public double CalculateLossProfit()
        {
            var pgr = playerGameRounds.Rounds.Single(f => f.Player.Name == targetPlayer.Name);

            switch (pgr.Result.ResultType)
            {
                case GameRoundPlayerResultType.None:
                    return 0;
                case GameRoundPlayerResultType.HackedVerloren:
                    return (roundPot.Value * 2 * -1).RoundTwoDigits();
                case GameRoundPlayerResultType.MitgegangenVerloren:
                    return (roundPot.Value * -1).RoundTwoDigits();
                case GameRoundPlayerResultType.HackedGewonnen:
                    return CalculateHackerEarnings();
                case GameRoundPlayerResultType.MitgegangenGewonnen:
                    return CalculateMitgegangenEarnings();
            }

            throw new Exception("Tra");
        }

        private double CalculateHackerEarnings()
        {
            if (playerGameRounds.AmountOfWinners == 1)
            {
                return roundPot.Value.RoundTwoDigits();
            }

            // Hacker always gets 2/3 of the pot
            var twoThirdsOfPot = roundPot.Value / 3 * 2;

            return twoThirdsOfPot.RoundTwoDigits();
        }

        private double CalculateMitgegangenEarnings()
        {
            if (playerGameRounds.AmountOfWinners == 1)
            {
                return roundPot.Value.RoundTwoDigits();
            }

            var hackerWon = playerGameRounds.HackerWon;

            if (hackerWon)
            {
                // Mitgegangen splittet immmer die restlichen 1/3 vom Pot
                var thirdOfPot = roundPot.Value / 3;
                var relativeAmount = thirdOfPot / playerGameRounds.AmountMitgegangenGewonnen;
                return relativeAmount.RoundTwoDigits();
            }

            // Hacker hat verloren, split even
            var relariveAmount = roundPot.Value / playerGameRounds.AmountMitgegangenGewonnen;
            return relariveAmount.RoundTwoDigits();
        }
    }
}