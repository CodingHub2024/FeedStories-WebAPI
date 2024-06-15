using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using System.Net;

namespace FeedStories.Common.TestData
{
    public static class StoryTestData
    {
        public static StoryDetailRequest storyDetailRequest = new StoryDetailRequest { StoryId = 1 };
        public static StoryDetailResponse storyDetailResponse = new StoryDetailResponse { StoryId = 1, Title = "title", Url = "https:\\localhost:4200" };
        public static StoryIdRequest storyIdRequest = new StoryIdRequest { PageNumber = 1, PageSize = 20 };
        public static StoryIdResponse storyIdResponse = new StoryIdResponse { StoryIds = new List<int> { 1, 2, 3 } };
        public static int HttpStatusCodeOk = 200;
    }
}