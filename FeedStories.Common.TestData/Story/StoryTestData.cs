using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using System.Net;
using System.Text.Json;

namespace FeedStories.Common.TestData
{
    public static class StoryTestData
    {
        public static StoryRequest StoryRequest = new StoryRequest { PageNumber = 0,PageSize =20 };
        public static StoryDetailResponse StoryDetailResponse = new StoryDetailResponse { StoryId = 1, Title = "title", Url = "https:\\localhost:4200" };
        public static StoryRequest StoryIdRequest = new StoryRequest { PageNumber = 1, PageSize = 20 };
        public static StoryResponse StoryResponse = new StoryResponse { Stories = [ new StoryDetailResponse { StoryId = 1, Title = "test", Url = "testURL" } ],TotalElements =500};
        public static Exception TestException = new ("Test exception");
        public static MemoryStream StoryResponseBodyStream = new();
        public static string StoryExpectedResponse = JsonSerializer.Serialize(new
        {
            ErrorMessage = "An internal server error has occured",
        });
    }
}