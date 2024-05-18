
using System.Net.Http.Json;

namespace FeedStories.Common.Utilities.Infrastructure
{
    /// <summary>
    /// HttpHelper class is used to interact with external services
    /// </summary>
    public class HttpHelper : IHttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ApplicationException($"Error fetching data from {url}: {ex.Message}", ex);
            }
        }
    }
}
