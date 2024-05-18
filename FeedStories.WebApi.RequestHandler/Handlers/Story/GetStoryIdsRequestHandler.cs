using FeedStories.Common.Utilities.Infrastructure;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting feed story ids
    /// </summary>
    public class GetStoryIdsRequestHandler : BaseRequestHandler<EmptyRequest, StoryIdResponse, GetStoryIdsRequestHandler>
    {
        private readonly IHttpHelper _httpHelper;
        private readonly IConfiguration _configuration;
        public GetStoryIdsRequestHandler(IHttpHelper httpHelper,IConfiguration configuration)
        {
            _httpHelper = httpHelper;
            _configuration = configuration;
        }

        public override async Task<StoryIdResponse> ProcessRequest(EmptyRequest request)
        {
            return new StoryIdResponse
            {
                StoryIds = await _httpHelper.GetAsync<List<int>>(_configuration["StoryURL:GetStoriesURL"])
            };
        }
    }
}