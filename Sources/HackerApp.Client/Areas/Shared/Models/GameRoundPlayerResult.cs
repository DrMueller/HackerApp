namespace HackerApp.Client.Areas.Shared.Models
{
    public class GameRoundPlayerResult
    {
        public double PlayerMoneyChanged { get; set; }

        public Player Player { get; set; }

        public GameRoundPlayerResultType ResultType { get; set; }
    }
}