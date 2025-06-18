using System.Text;
using System.Text.Json;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using HackerApp.Client.Infrastructure.State.Dtos;
using HackerApp.Client.Infrastructure.State.Services.Servants;
using HackerApp.Infrastructure.SemKer.Models;
using HackerApp.Infrastructure.Settings.Provisioning.Services;
using Microsoft.SemanticKernel;

namespace HackerApp.Infrastructure.SemKer.Services.Implementation
{
    public class GameAnalyzer(ISettingsProvider settingsProvider, IGameMapper gameMapper) : IGameAnalyzer
    {
        private const string DeplyomentName = "gpt-4.1";

        private const string HackenDescription = "Hacken ist ein Spiel, bei dem Hacker versuchen, Geld (in Schweizer Franken) zu gewinnen, indem sie gegen andere Spieler eine Art Jass spielen. Das Spiel wird in Runden gespielt, und jede Runde hat einen Pot, der unter den Spielern aufgeteilt wird. Die Spieler können entweder selbst hacken oder sich an den Hacks anderer Spieler mitgehen. Hacker müssen zwei Stiche machen. Die die mitgehen müssen einen Stich machen. Der Hacker bekommt 2/3 des Pots, die Mitgehen teilen die restlichen 1/3 des Pots unter sich. Das Ziel des Spiels ist es, am Ende des Spiels das meiste Geld zu haben.";

        private const string Message = $"Analysier nachfolgendes Spiel und verteile Shots anhand von folgenden Merkmalen: Die Spieler mit den meisten Bussen, die am wenigsten spielen und am meisten gewinnen, müssen die meisten Shots zahlen. Die Spieler die oft spielen und/oder verlieren müssen die wenigsten zahlen. Jeder Spieler kann maxiumal drei Shots und muss mininmum keinen Shot zahlen. Wähl aus folgenden Shots zufällig einen aus {Shots} und gib allen pro Berechnung den gleichen Shot";

        private const string ModelId = "gpt-4.1";

        private const string Output = "Erstell eine kurze Zusammenfassung des Spielverlaufs gefolgt von einer Tabelle. Mach am Schluss eine kurze Auflistung pro Spieler, wie du sein Verhalten interpretiet hast und wieso er so und so viele Shots bekommt. Formatier alles in HTML exklusive die HTML-Tags. Das HTML wird via Bootstrap in Darkmode in einem Modal-Fenster dargestellt, optimier die Aufgabe auf das.";

        private const string Shots = "Jägermeister, Tequilla, Ouzo, Whisky, B52, Vodka, Cognac";

        public async Task<string> AnalyzeAsync(GameDto gameDto)
        {
            var gameToAnalyze = CreateGameToAnalyze(gameDto);
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddAzureOpenAIChatCompletion(
                DeplyomentName,
                settingsProvider.AppSettings.OpenAiEndpoint,
                settingsProvider.AppSettings.OpenAiKey,
                modelId: ModelId);

            var kernel = kernelBuilder.Build();

            StringBuilder chatPrompt = new($"""
                                            <message role="system">{HackenDescription}</message>
                                            <message role="system">{Message}</message>
                                            <message role="system">{Output}</message>
                                            <message role="system">Nachfolgend erhälst du das Game JSON</message>
                                            <message role="system">{JsonSerializer.Serialize(gameToAnalyze)}</message>
                                            """);

            var reply = await kernel.InvokePromptAsync(chatPrompt.ToString());

            return reply.ToString();
        }

        private GameToAnalyze CreateGameToAnalyze(GameDto dto)
        {
            var game = gameMapper.Map(dto);

            var statistik = new List<SpielerStatistik>();

            foreach (var spieler in game.Players)
            {
                var overallLossProfit = spieler.CalculateLossProfit(game.GameRounds, LossProfitType.GameWins);
                var overallPenalties = spieler.CalculateLossProfit(game.GameRounds, LossProfitType.Penalties) * -1;
                statistik.Add(new SpielerStatistik(spieler.Name, overallLossProfit, overallPenalties));
            }

            var rounds = new List<Spielrunde>();
            foreach (var r in game.GameRounds)
            {
                var ssp = new List<SpielerSpielrunde>();

                foreach (var spielerGameRound in r.PlayerGameRounds)
                {
                    ssp.Add(new SpielerSpielrunde(
                        spielerGameRound.Player.Name,
                        r.CalculcateEarnings(spielerGameRound.Player),
                        spielerGameRound.Penalty?.PenaltyValue ?? 0,
                        spielerGameRound.Result.ResultType == GameRoundPlayerResultType.HackedGewonnen || spielerGameRound.Result.ResultType == GameRoundPlayerResultType.HackedVerloren,
                        spielerGameRound.Result.ResultType == GameRoundPlayerResultType.MitgegangenGewonnen || spielerGameRound.Result.ResultType == GameRoundPlayerResultType.MitgegangenVerloren,
                        spielerGameRound.Result.ResultType == GameRoundPlayerResultType.None));
                }

                rounds.Add(new Spielrunde(r.RoundNumber, r.RoundPot.Value, ssp));
            }

            return new GameToAnalyze(statistik, rounds);
        }
    }
}