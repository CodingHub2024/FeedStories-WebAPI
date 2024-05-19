using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting feed story ids
    /// </summary>
    public class GetStoryIdsRequestHandler : BaseRequestHandler<EmptyRequest, StoryIdResponse, GetStoryIdsRequestHandler>
    {
        private readonly IStoryService _storyService;
        public GetStoryIdsRequestHandler(ILogger<GetStoryIdsRequestHandler> logger,IStoryService storyService):base(logger) 
        {
            _storyService = storyService;
        }

        public override async Task<StoryIdResponse> ProcessRequest(EmptyRequest request)
        {
            _logger.LogDebug($"Called {nameof(GetStoryIdsRequestHandler)} ProcessRequest");

            return new StoryIdResponse
            {
                StoryIds = await _storyService.GetStoryIds()
            };
        }
    }
}