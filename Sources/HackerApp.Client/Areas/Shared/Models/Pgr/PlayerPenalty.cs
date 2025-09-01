namespace HackerApp.Client.Areas.Shared.Models.Pgr
{
    public record PlayerPenalty(
        string PlayerName,
        double PenaltyValue,
        bool ApplyPenaltyNextRound);
}
