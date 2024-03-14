namespace HackerApp.Client.Areas.Shared.Models
{
    public class Game(IReadOnlyCollection<Player> players)
    {
        private readonly List<GameRound> _rounds = new();

        public IReadOnlyCollection<Player> Players { get; } = players;

        public IReadOnlyCollection<GameRound> Rounds => _rounds;

        public void AddNewRound(double roundEinsatz)
        {
            _rounds.Insert(0, GameRound.Create(roundEinsatz, Players, _rounds.FirstOrDefault()));
        }
    }
}