using System.Net.Http.Json;
using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;

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
        //private readonly AsyncCachePolicy<StoryDetailResponse> _cachePolicy;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);
        private readonly string? _storyBaseURL = string.Empty;

        public StoryService(HttpClient httpClient, IConfiguration configuration, IMemoryCache cache, ILogger<StoryService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
            _logger = logger;
            _cache = cache;
            _storyBaseURL = _configuration["StoryURL:GetStoryDetailsURL"];

            //var cacheProvider = new MemoryCacheProvider(memoryCache);

            //_cachePolicy = Policy.CacheAsync<StoryDetailResponse>(
            //    cacheProvider.AsyncFor<StoryDetailResponse>(),
            //    TimeSpan.FromSeconds(10000) // Short expiration time for live data
            //);
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
        public async Task<StoryDetailResponse?> GetStoryDetails(int storyId)
        {
            var cacheKey = $"StoryDetail_{storyId}";

            if (!_cache.TryGetValue(cacheKey, out StoryDetailResponse? storyDetailsResponse))
            {
                _logger.LogInformation($"Cache miss for story ID {storyId}. Fetching from external service...");

                var storyDetailsURL = _storyBaseURL;
                storyDetailsURL = storyDetailsURL?.Replace("@id", Uri.EscapeDataString(storyId.ToString()));

                if (string.IsNullOrWhiteSpace(storyDetailsURL))
                {
                    throw new InvalidOperationException("Story details URL template is not configured.");
                }

                var response = await _httpClient.GetAsync(storyDetailsURL);
                response.EnsureSuccessStatusCode();

                storyDetailsResponse = await response.Content.ReadFromJsonAsync<StoryDetailResponse>();

                _cache.Set(cacheKey, storyDetailsResponse, _cacheExpiration);

            }

            return String.IsNullOrEmpty(storyDetailsResponse?.Url) ? null : storyDetailsResponse;
        }
    }
}