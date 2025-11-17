namespace HackerApp.Client.Areas.Shared.Models.Pgr
{
    public class GameRoundPlayerResult
    {
        public bool HasWon =>
            ResultType == GameRoundPlayerResultType.HackedGewonnen ||
            ResultType == GameRoundPlayerResultType.MitgegangenGewonnen ||
            ResultType == GameRoundPlayerResultType.HackedGewonnenSafer;

        public GameRoundPlayerResultType ResultType { get; set; } = GameRoundPlayerResultType.None;
    }
}