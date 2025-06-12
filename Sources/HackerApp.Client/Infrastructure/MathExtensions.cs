namespace HackerApp.Client.Infrastructure
{
    public static class MathExtensions
    {
        public static double RoundTwoDigits(this double val)
        {
            return Math.Round(val, 2);
        }

        public static string ToFranken(this double amount)
        {
            return $"{amount.RoundToNext50Rappen():0.00} Fr.";
        }

        private static double RoundToNext50Rappen(this double amount)
        {
            return Math.Ceiling(amount * 2) / 2;
        }
    }
}