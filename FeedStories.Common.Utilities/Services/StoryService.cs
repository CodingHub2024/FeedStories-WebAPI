
using System.Net.Http.Json;
using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly.Caching.Memory;
using Polly;
using Polly.Caching;

namespace FeedStories.Common.Utilities.Services
{
    /// <summary>
    /// StoryService class is used to interact with external story services
    /// </summary>
    public class StoryService : IStoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        ILogger<StoryService> _logger;
        private readonly AsyncCachePolicy<StoryDetailResponse> _cachePolicy;

        public StoryService(HttpClient httpClient, IConfiguration configuration, IMemoryCache memoryCache, ILogger<StoryService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
            _logger = logger;

            var cacheProvider = new MemoryCacheProvider(memoryCache);

            _cachePolicy = Policy.CacheAsync<StoryDetailResponse>(
                cacheProvider.AsyncFor<StoryDetailResponse>(),
                TimeSpan.FromSeconds(1) // Short expiration time for live data
            );
        }

        /// <summary>
        /// GetStoryIds is used to get storyids
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<int>> GetStoryIds()
        {
            HttpResponseMessage? response = null;
            try
            {
                response = await _httpClient.GetAsync(_configuration["StoryURL:GetStoriesURL"]);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<int>>();
            }
            finally
            {
                response?.Dispose();
            }
        }

        /// <summary>
        /// GetStoryDetail is used to get the entire story details base on id
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public async Task<StoryDetailResponse> GetStoryDetails(int storyId)
        {
            var cacheKey = $"StoryDetail_{storyId}";
            var storyDetailsURL = _configuration["StoryURL:GetStoryDetailsURL"];

            if (string.IsNullOrEmpty(storyDetailsURL))
            {
                throw new InvalidOperationException("Story details URL template is not configured.");
            }

            storyDetailsURL = storyDetailsURL.Replace("@id", Uri.EscapeDataString(storyId.ToString()));


            return await _cachePolicy.ExecuteAsync(async context =>
            {
                _logger.LogInformation($"Cache miss for story ID {storyId}. Fetching from external service...");

                var response = await _httpClient.GetAsync(storyDetailsURL);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<StoryDetailResponse>();
            }, new Context(cacheKey));
        }
    }
}