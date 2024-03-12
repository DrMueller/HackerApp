namespace HackerApp.Client.Areas.Shared.Models
{
    public class GameRound
    {
        public required double UsedEinsatz { get; set; }

        public required double Pot { get; init; }
        public required IReadOnlyCollection<GameRoundPlayerResult> Results { get; set; }
    }
}