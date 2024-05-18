using FeedStories.Common.Utilities.Infrastructure;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using System.Security.Cryptography;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting feed ids
    /// </summary>
    public class GetStoryIdsRequestHandler : BaseRequestHandler<EmptyRequest, StoryIdResponse, GetStoryIdsRequestHandler>
    {
        private readonly IHttpHelper _httpHelper;
        public GetStoryIdsRequestHandler(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public override async Task<StoryIdResponse> ProcessRequest(EmptyRequest request)
        {
            return new StoryIdResponse
            {
                StoryIds = await _httpHelper.GetAsync<List<int>>("https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty")
            };
        }
    }
}