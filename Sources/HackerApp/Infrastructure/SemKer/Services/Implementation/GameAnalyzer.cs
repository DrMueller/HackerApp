using System.Text;
using System.Text.Json;
using HackerApp.Client.Infrastructure.State.Dtos;
using HackerApp.Infrastructure.Settings.Provisioning.Services;
using Microsoft.SemanticKernel;

namespace HackerApp.Infrastructure.SemKer.Services.Implementation
{
    public class GameAnalyzer(ISettingsProvider settingsProvider) : IGameAnalyzer
    {
        private const string DeplyomentName = "gpt-4.1";

        private const string GameDtoDescription = "Interpretier das JSON wie folgt: Jede PlayerGameRound stellt ein Spieler pro Runde dar. Wenn der ResultType \"None\" ist, hat der Spieler nicht teilngenommen an der Runde.";

        private const string HackenDescription = "Hacken ist ein Spiel, bei dem Hacker versuchen, Geld zu gewinnen, indem sie gegen andere Spieler eine Art Jass spielen. Das Spiel wird in Runden gespielt, und jede Runde hat einen Pot, der unter den Spielern aufgeteilt wird. Die Spieler können entweder selbst hacken oder sich an den Hacks anderer Spieler mitgehen. Hacker müssen zwei Stiche machen. Die die mitgehen müssen einen Stich machen. Der Hacker bekommt 2/3 des Pots, die Mitgehen teilen die restlichen 1/3 des Pots unter sich. Das Ziel des Spiels ist es, am Ende des Spiels das meiste Geld zu haben.";

        private const string Message = $"Analysier nachfolgendes Spiel und verteile Shots anhand von folgenden Merkmalen: Die Spieler mit den meisten Bussen, die am wenigsten spielen und/oder am meisten gewinnen, erhalten die meisten Shots. Die Spieler die oft spielen und/oder verlieren erhalten die wenigsten. Wähl aus folgenden Shots zufällig einen aus {Shots} und gib allen pro Berechnung den gleichen Shot";

        private const string ModelId = "gpt-4.1";

        private const string Output = "Erstell eine kurze Zusammenfassung des Spielverlaufs gefolgt von einer Tabelle. Formatier alles in HTML. Das HTML wird via Bootstrap in Darkmode in einem Modal-Fenster dargestellt, optimier die Aufgabe auf das.";

        private const string Shots = "Jägermeister, Tequilla, Sake, Ouzo, Whisky, B52, Vodka, Cognac";

        public async Task<string> AnalyzeAsync(GameDto game)
        {
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
                                            <message role="system">{GameDtoDescription}</message>
                                            <message role="system">{Output}</message>
                                            <message role="system">Nachfolgend erhälst du das Game JSON</message>
                                            <message role="system">{JsonSerializer.Serialize(game)}</message>
                                            """);

            var reply = await kernel.InvokePromptAsync(chatPrompt.ToString());

            return reply.ToString();
        }
    }
}