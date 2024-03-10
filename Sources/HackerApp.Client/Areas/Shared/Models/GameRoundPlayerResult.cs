namespace HackerApp.Client.Areas.Shared.Models
{
    public class GameRoundPlayerResult
    {
        public Player Player { get; set; }
        
        public double MoneyAfterResult { get; set; }

        public double InitialMouney { get; set; }
        
        public GameRoundPlayerResultType ResultType { get; set; }
    }
}