using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using System.Net;
using System.Text.Json;

namespace FeedStories.Common.TestData
{
    public static class StoryTestData
    {
        public static StoryDetailRequest StoryDetailRequest = new StoryDetailRequest { StoryId = 1 };
        public static StoryDetailResponse StoryDetailResponse = new StoryDetailResponse { StoryId = 1, Title = "title", Url = "https:\\localhost:4200" };
        public static StoryIdRequest StoryIdRequest = new StoryIdRequest { PageNumber = 1, PageSize = 20 };
        public static StoryIdResponse StoryIdResponse = new StoryIdResponse { StoryIds = new List<int> { 1, 2, 3 } };
        public static Exception TestException = new ("Test exception");
        public static MemoryStream StoryResponseBodyStream = new();
        public static string StoryExpectedResponse = JsonSerializer.Serialize(new
        {
            ErrorMessage = "An internal server error has occured",
        });
    }
}