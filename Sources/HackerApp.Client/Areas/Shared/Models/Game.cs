namespace HackerApp.Client.Areas.Shared.Models
{
    public class Game
    {
        private readonly List<GameRound> _rounds;

        public Game(IReadOnlyCollection<Player> players)
        {
            Players = players;
            _rounds = new List<GameRound>();
        }

        public IReadOnlyCollection<Player> Players { get; }

        public IReadOnlyCollection<GameRound> Rounds => _rounds;

        public void NewRound(double einsatz)
        {
            var lastRound = Rounds.First();
            var loosingRound = lastRound.Results.Any(f => f.ResultType == GameRoundPlayerResultType.HackedVerloren || f.ResultType == GameRoundPlayerResultType.MitgegangenVerloren);

            if (!loosingRound)
            {
                InitializeRoundWithEinsatz(lastRound, einsatz);
                return;
            }

            var newResults = new List<GameRoundPlayerResult>();

            newResults.AddRange(CreateLoosers(lastRound));
            newResults.AddRange(CreateWinners(lastRound));
            newResults.AddRange(CreateNotParicipated(lastRound));

            var newPot = newResults
                .Where(f => f.PlayerMoneyChanged > 0)
                .Sum(f => f.PlayerMoneyChanged);

            var newRound = new GameRound
            {
                UsedEinsatz = einsatz,
                Results = newResults,
                Pot = newPot
            };

            _rounds.Insert(0, newRound);
        }

        private IEnumerable<GameRoundPlayerResult> CreateLoosers(GameRound lastRound)
        {
            var result = new List<GameRoundPlayerResult>();

            foreach (var res in lastRound.Results)
            {
                switch (res.ResultType)
                {
                    case GameRoundPlayerResultType.HackedVerloren:
                    {
                        var moneyToPay = lastRound.Pot * 2;
                        result.Add(new GameRoundPlayerResult
                        {
                            Player = res.Player,
                            PlayerMoneyChanged = moneyToPay,
                            ResultType = GameRoundPlayerResultType.None
                        });

                        break;
                    }

                    case GameRoundPlayerResultType.MitgegangenVerloren:
                    {
                        var moneyToPay = lastRound.Pot;

                        result.Add(new GameRoundPlayerResult
                        {
                            Player = res.Player,
                            PlayerMoneyChanged = moneyToPay,
                            ResultType = GameRoundPlayerResultType.None
                        });

                        break;
                    }
                }
            }

            return result;
        }

        private IEnumerable<GameRoundPlayerResult> CreateNotParicipated(GameRound lastRound)
        {
            return lastRound
                .Results
                .Where(f => f.ResultType == GameRoundPlayerResultType.None)
                .Select(f => new GameRoundPlayerResult
                {
                    Player = f.Player,
                    ResultType = GameRoundPlayerResultType.None,
                    PlayerMoneyChanged = 0
                });
        }

        private IEnumerable<GameRoundPlayerResult> CreateWinners(GameRound lastRound)
        {
            var winners = lastRound.Results.Where(f => f.ResultType is GameRoundPlayerResultType.HackedGewonnen or GameRoundPlayerResultType.MitgegangenGewonnen);

            var potParts = lastRound.Pot / (winners.Count() + 1);

            var result = new List<GameRoundPlayerResult>();

            foreach (var winner in winners)
            {
                var wonMoney = winner.ResultType == GameRoundPlayerResultType.HackedGewonnen ? potParts * 2 : potParts;

                result.Add(new GameRoundPlayerResult
                {
                    Player = winner.Player,
                    ResultType = GameRoundPlayerResultType.None,
                    PlayerMoneyChanged = wonMoney * -1
                });
            }

            return result;
        }

        private void InitializeRoundWithEinsatz(GameRound lastRound, double einsatz)
        {
            var newResults = lastRound.Results.Select(f => new GameRoundPlayerResult
            {
                Player = f.Player,
                PlayerMoneyChanged = einsatz,
                ResultType = GameRoundPlayerResultType.None
            }).ToList();

            double newPot;
            // Jemand hat gewonnen, neuer Pot
            if (lastRound.Results.Any(f => f.ResultType == GameRoundPlayerResultType.HackedGewonnen || f.ResultType == GameRoundPlayerResultType.MitgegangenGewonnen))
            {
                newPot = newResults.Sum(f => f.PlayerMoneyChanged);
            }
            else
            {
                newPot = lastRound.Pot + newResults.Sum(f => f.PlayerMoneyChanged);
            }

            var newRound = new GameRound
            {
                UsedEinsatz = einsatz,
                Results = newResults,
                Pot = newPot
            };

            _rounds.Insert(0, newRound);
        }
    }
}