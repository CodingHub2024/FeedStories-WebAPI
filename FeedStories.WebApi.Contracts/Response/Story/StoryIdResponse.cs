namespace FeedStories.WebApi.Contracts.Response
{
    /// <summary>
    /// StoryIdResponse is response for getting story ids
    /// </summary>
    public class StoryIdResponse
    {
        public int TotalElements { get; set; }
        public IEnumerable<int>? StoryIds { get; set; }
    }
}
