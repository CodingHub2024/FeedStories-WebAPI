namespace FeedStories.WebApi.RequestHandler
{
    /// <summary>
    /// IRequestHandlerFactory defines the method which calls respective request handler
    /// </summary>
    public interface IRequestHandlerFactory
    {
        Task<TResponse> ProcessRequest<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class;
    }
}
