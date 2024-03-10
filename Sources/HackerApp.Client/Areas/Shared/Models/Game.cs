namespace HackerApp.Client.Areas.Shared.Models
{
    public class Game
    {
        public List<Player> Players { get; set; }

        public List<GameRound> Rounds { get; set; }

        public void NewRound(double einsatz)
        {
            var lastRound = Rounds.First();
            var loosingRound = lastRound.Results.Any(f => f.ResultType == GameRoundPlayerResultType.HackedVerloren || f.ResultType == GameRoundPlayerResultType.MitgegangenVerloren);

            if (!loosingRound)
            {
                var newRound = new GameRound();
                var newPlayerMoney = lastRound.Results.Select(f => new GameRoundPlayerResult
                {
                    Player = f.Player,
                    InitialMouney = einsatz,
                    ResultType = GameRoundPlayerResultType.None,
                }).ToList();

                Rounds.Insert(0, newRound);
            }

            //var newPot = lastRound.FinalPot + (einsatz * lastRound.Results.Count);

            //Rounds.Insert(0, new GameRound
            //{
            //    FinalPot = newPot,
            //    Results = newPlayerMoney
            //});
        }
    }
}