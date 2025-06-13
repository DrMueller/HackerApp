using Microsoft.SemanticKernel;

namespace HackerApp.Client.Infrastructure.SemKer.Services.Implementation
{
    public class GameAnalyzer : IGameAnalyzer
    {
        private const 

        public Task AnalyzeAsync()
        {
            var builder = Kernel.CreateBuilder();
            kernelBuilder.AddAzureOpenAIChatCompletion(
                deploymentName: TestConfiguration.AzureOpenAI.ChatDeploymentName,
                endpoint: TestConfiguration.AzureOpenAI.Endpoint,
                credentials: new DefaultAzureCredential(),
                modelId: TestConfiguration.AzureOpenAI.ChatModelId);
        }
    }
}
