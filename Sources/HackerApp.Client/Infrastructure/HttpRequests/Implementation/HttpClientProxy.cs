using System.Text;
using System.Text.Json;
using HackerApp.Client.Infrastructure.HttpRequests.Servants;
using JetBrains.Annotations;

namespace HackerApp.Client.Infrastructure.HttpRequests.Implementation
{
    [UsedImplicitly]
    internal class HttpClientProxy(IHttpCallHandler callHandler) : IHttpClientProxy
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<T> DeleteAsync<T>(string url)
        {
            using var response = await callHandler.DeleteAsync(url);
            var stringContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<T>(stringContent, _options)!;
            return result;
        }

        public async Task<T> GetAsync<T>(string relativeUrl)
        {
            var result = await callHandler.GetFromJsonAsync<T>(relativeUrl);

            return result!;
        }

        public async Task<TResult> PostAsync<TResult, T>(string relativeUrl, T dto)
        {
            using var response = await callHandler.PostAsJsonAsync(relativeUrl, dto);
            var stringContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TResult>(stringContent, _options)!;

            return result;
        }

        public async Task<bool> SendAsync(string url, HttpMethod method, string json)
        {
            using var request = new HttpRequestMessage(method, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var result = await callHandler.SendAsync(request);

            return result.IsSuccessStatusCode;
        }
    }
}