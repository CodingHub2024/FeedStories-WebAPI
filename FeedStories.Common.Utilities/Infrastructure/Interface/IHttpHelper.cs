using System.Net.Http;

namespace FeedStories.Common.Utilities.Infrastructure
{
    /// <summary>
    /// Interface is used for interacting with external services
    /// </summary>
    public interface IHttpHelper
    {
        Task<T> GetAsync<T>(string url);
        void SetBaseAddress(string baseAddress);
    }
}
