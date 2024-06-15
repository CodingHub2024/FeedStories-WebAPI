using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting story details
    /// </summary>
    public class GetStoryDetailsRequestHandler : BaseRequestHandler<StoryDetailRequest, StoryDetailResponse, GetStoryDetailsRequestHandler>
    {

        private readonly IStoryService _storyService;
        public GetStoryDetailsRequestHandler(ILogger<GetStoryDetailsRequestHandler> logger, IStoryService storyService) : base(logger)
        {
            _storyService = storyService;
        }

        public override async Task<StoryDetailResponse?> ProcessRequest(StoryDetailRequest request)
        {
            _logger.LogDebug($"Called {nameof(GetStoryDetailsRequestHandler)} ProcessRequest({request.StoryId})");

            var response = await _storyService.GetStoryDetails(request.StoryId);

            if (response != null)
            {
                response.StoryId = request.StoryId;
            }

            return response;
        }
    }
}