namespace HackerApp.Client.Areas.Shared.Models
{
    public class Game(
        IReadOnlyCollection<Player> players,
        IList<GameRound> rounds)
    {
        public Game(IReadOnlyCollection<Player> players)
            : this(players, new List<GameRound>())
        {
        }

        public IReadOnlyCollection<GameRound> GameRounds => rounds.AsReadOnly();

        public IReadOnlyCollection<Player> Players { get; } = players;

        public void AddNewRound(double roundEinsatz)
        {
            rounds.Insert(0, GameRound.Create(roundEinsatz, Players, rounds.FirstOrDefault()));
        }
    }
}