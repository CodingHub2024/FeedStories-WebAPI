using FeedStories.WebApi.Contracts.Response;

namespace FeedStories.Common.Utilities.Interface
{
    /// <summary>
    /// IStoryService interface is used for interacting with external story services
    /// </summary>
    public interface IStoryService
    {
        Task<List<int>> GetStoryIds();
        Task<StoryDetailResponse> GetStoryDetails(int storyId);
    }
}