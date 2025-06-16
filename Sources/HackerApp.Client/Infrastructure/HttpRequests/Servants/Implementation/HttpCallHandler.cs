using System.Net;
using System.Net.Http.Json;
using HackerApp.Client.Areas.Login;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Infrastructure.HttpRequests.Servants.Implementation
{
    [UsedImplicitly]
    public class HttpCallHandler(IHttpClientFactory httpClientFactory, NavigationManager navigator) : IHttpCallHandler
    {
        public const string HttpClientName = "Auth";

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await HandledCallAsync(client => client.DeleteAsync(url));
        }

        public async Task<T> GetFromJsonAsync<T>(string url)
        {
            using var response = await HandledCallAsync(client => client.GetAsync(url));
            var result = await response.Content.ReadFromJsonAsync<T>();

            return result!;
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T dto)
        {
            return await HandledCallAsync(client => client.PostAsJsonAsync(url, dto));
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await HandledCallAsync(async client => await client.SendAsync(request));
        }

        private HttpClient CreateClient()
        {
            var client = httpClientFactory.CreateClient(HttpClientName);

            client.BaseAddress = new Uri(navigator.BaseUri);

            return client;
        }

        private async Task<HttpResponseMessage> HandledCallAsync(Func<HttpClient, Task<HttpResponseMessage>> callback)
        {
            using var httpClient = CreateClient();
            var result = await callback(httpClient);

            if (result.IsSuccessStatusCode)
            {
                return result;
            }

            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                navigator.NavigateTo(LoginPage.Path, true);
            }

            return result;
        }
    }
}