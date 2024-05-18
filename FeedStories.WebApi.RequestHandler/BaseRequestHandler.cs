
namespace FeedStories.WebApi.RequestHandler
{
    /// <summary>
    /// Base Request Handler
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TRequestHandler"></typeparam>
    public abstract class BaseRequestHandler<TRequest, TResponse, TRequestHandler> : IRequestHandler<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
        where TRequestHandler : class
    {
        public abstract Task<TResponse> ProcessRequest(TRequest request);
    }
}
