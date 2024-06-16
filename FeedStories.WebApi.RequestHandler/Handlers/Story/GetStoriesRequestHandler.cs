using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting feed story ids
    /// </summary>
    public class GetStoriesRequestHandler : BaseRequestHandler<StoryRequest, StoryResponse, GetStoriesRequestHandler>
    {
        private readonly IStoryService _storyService;
        public GetStoriesRequestHandler(ILogger<GetStoriesRequestHandler> logger, IStoryService storyService) : base(logger)
        {
            _storyService = storyService;
        }

        public override async Task<StoryResponse?> ProcessRequest(StoryRequest request)
        {
            _logger.LogDebug($"Called {nameof(GetStoriesRequestHandler)} ProcessRequest");

            var StoryIds = await _storyService.GetStoryIds();

            // Create a list of story fetching tasks to fetch details concurrently
            var detailTasks = StoryIds.Skip(request.PageNumber * request.PageSize).Take(request.PageSize)
                .Select(storyId => GetStoryDetails(storyId)).ToList();
            
            return new StoryResponse
            {
                // Wait for all tasks to complete
                StoryDetails = await Task.WhenAll(detailTasks),
                TotalElements = StoryIds.Count()
            };

        }

        /// <summary>
        /// Get story details by story id
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        private async Task<StoryDetailResponse?> GetStoryDetails(int storyId)
        {
            var response = await _storyService.GetStoryDetails(storyId);

            if (response != null)
            {
                response.StoryId = storyId;
            }

            return response;
        }
    }
}