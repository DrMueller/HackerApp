namespace HackerApp.Client.Areas.Shared.Models;

public class Game
{
    public List<Player> Players { get; set; }

    public List<GameRound> Rounds { get; set; }

    public void AddSaetzu(double einsatz)
    {
        var lastRound = Rounds.First();
        var newPlayerMoney = lastRound.PlayerMoney.Select(f => new PlayerMoney
        {
            Money = f.Money - einsatz,
            Player = f.Player
        }).ToList();

        var newPot = lastRound.Pot + (einsatz * lastRound.PlayerMoney.Count);

        Rounds.Insert(0, new GameRound
        {
            Pot = newPot,
            PlayerMoney = newPlayerMoney
        });
    }
}