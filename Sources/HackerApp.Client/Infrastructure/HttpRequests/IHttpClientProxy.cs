namespace HackerApp.Client.Infrastructure.HttpRequests
{
    public interface IHttpClientProxy
    {
        Task<T> DeleteAsync<T>(string url);
        Task<T> GetAsync<T>(string url);

        Task<TResult> PostAsync<TResult, T>(string relativeUrl, T dto);

        Task<bool> SendAsync(string url, HttpMethod method, string json);
    }
}