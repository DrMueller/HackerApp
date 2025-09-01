namespace HackerApp.Client.Areas.Shared.Models.Pgr
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
    }
}