using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting story details
    /// </summary>
    public class GetStoryDetailsRequestHandler : BaseRequestHandler<StoryIdRequest, StoryDetailResponse, GetStoryDetailsRequestHandler>
    {

        private readonly IStoryService _storyService;
        public GetStoryDetailsRequestHandler(ILogger<GetStoryDetailsRequestHandler> logger, IStoryService storyService) : base(logger)
        {
            _storyService = storyService;
        }

        public override async Task<StoryDetailResponse> ProcessRequest(StoryIdRequest request)
        {
            _logger.LogDebug($"Called {nameof(GetStoryDetailsRequestHandler)} ProcessRequest({request.StoryId})");

            // Validate and sanitize storyId
            if (!int.TryParse(request.StoryId.ToString(), out int validStoryId))
            {
                throw new ArgumentException("Invalid story ID");
            }

            var response = await _storyService.GetStoryDetails(validStoryId);

            if (string.IsNullOrWhiteSpace(response?.Url))
            {
                return null; 
            }

            response.StoryId = request.StoryId;
            return response;
        }
    }
}