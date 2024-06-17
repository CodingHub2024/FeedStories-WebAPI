using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using System.Net;
using System.Text.Json;

namespace FeedStories.Common.TestData
{
    public static class StoryTestData
    {
        public static StoryRequest StoryRequest = new StoryRequest { PageNumber = 0, PageSize = 20 };
        public static StoryResponse StoryResponse = new StoryResponse { Stories = [new StoryDetailResponse { StoryId = 1, Title = "test", Url = "testURL" }], TotalElements = 500 };
        public static Exception TestException = new("Test exception");
        public static MemoryStream StoryResponseBodyStream = new();
        public static string StoryExpectedResponse = JsonSerializer.Serialize(new
        {
            ErrorMessage = "An internal server error has occured",
        });

        public static List<int> StoryIds = new List<int> { 1, 2 };

        public static StoryDetailResponse StoryDetailResponse = new StoryDetailResponse { StoryId = 1, Title = "title", Url = "https:\\localhost:4200" };
        public static StoryDetailResponse StoryDetailNullResponse = null;
        public static int TotalElements = 2;
        public static int StoryCount = 0;
    }
}