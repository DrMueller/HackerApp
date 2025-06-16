namespace HackerApp.Client.Infrastructure.HttpRequests.Servants
{
    public interface IHttpCallHandler
    {
        Task<HttpResponseMessage> DeleteAsync(string relativeUrl);
        Task<T> GetFromJsonAsync<T>(string relativeUrl);
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string relativeUrl, T dto);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}