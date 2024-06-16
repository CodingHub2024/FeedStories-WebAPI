namespace FeedStories.WebApi.Contracts.Response
{
    public class StoryResponse
    {
        public int TotalElements { get; set; }
        public StoryDetailResponse[]? StoryDetails { get; set; }
    }
}