using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting feed story ids
    /// </summary>
    public class GetStoryIdsRequestHandler : BaseRequestHandler<StoryIdRequest, StoryIdResponse, GetStoryIdsRequestHandler>
    {
        private readonly IStoryService _storyService;
        public GetStoryIdsRequestHandler(ILogger<GetStoryIdsRequestHandler> logger,IStoryService storyService):base(logger) 
        {
            _storyService = storyService;
        }

        public override async Task<StoryIdResponse> ProcessRequest(StoryIdRequest request)
        {
            _logger.LogDebug($"Called {nameof(GetStoryIdsRequestHandler)} ProcessRequest");

           var StoryIds = await _storyService.GetStoryIds();

            return new StoryIdResponse
            {
                StoryIds = StoryIds.Skip(request.PageNumber*request.PageSize).Take(request.PageSize),
                TotalElements = StoryIds.Count()
            };
        }
    }
}