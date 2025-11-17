namespace HackerApp.Client.Areas.Shared.Models.Pgr
{
    public class PlayerGameRounds(IReadOnlyCollection<PlayerGameRound> rounds)
    {
        public IReadOnlyCollection<PlayerGameRound> Rounds { get; } = rounds;
        public bool EinsatzWasPaid => Rounds.All(x => x.Result.ResultType != GameRoundPlayerResultType.MitgegangenVerloren && x.Result.ResultType != GameRoundPlayerResultType.HackedVerloren);
        public int AmountOfWinners => Rounds.Count(f => f.Result.HasWon);
        public bool HackerWon => Rounds.Any(f => f.Result.ResultType is GameRoundPlayerResultType.HackedGewonnen or GameRoundPlayerResultType.HackedGewonnenSafer);
        public int AmountMitgegangenGewonnen => Rounds.Count(f => f.Result.ResultType == GameRoundPlayerResultType.MitgegangenGewonnen);
    }
}
