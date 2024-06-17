namespace FeedStories.WebApi.Contracts.Response
{
    public class StoryResponse
    {
        public int TotalElements { get; set; }
        public IEnumerable<StoryDetailResponse>? Stories { get; set; }
    }
}