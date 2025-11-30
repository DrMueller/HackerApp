namespace HackerApp.Client.Areas.Shared.Models.Pgr
{
    public class GameRoundPlayerResult
    {
        public bool IsHacker =>
            ResultType == GameRoundPlayerResultType.HackedGewonnen ||
            ResultType == GameRoundPlayerResultType.HackedVerloren ||
            ResultType == GameRoundPlayerResultType.HackedGewonnenSafer;

        public bool HasWon =>
            ResultType == GameRoundPlayerResultType.HackedGewonnen ||
            ResultType == GameRoundPlayerResultType.MitgegangenGewonnen ||
            ResultType == GameRoundPlayerResultType.HackedGewonnenSafer;

        public GameRoundPlayerResultType ResultType { get; set; } = GameRoundPlayerResultType.None;
    }
}