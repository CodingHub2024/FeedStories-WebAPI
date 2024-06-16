namespace FeedStories.WebApi.Contracts.Response
{
    public class StoryResponse
    {
        public int TotalElements { get; set; }
        public StoryDetailResponse[]? Stories { get; set; }
    }
}