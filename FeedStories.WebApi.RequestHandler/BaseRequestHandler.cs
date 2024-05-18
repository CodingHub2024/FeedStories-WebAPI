
namespace FeedStories.WebApi.RequestHandler
{
    public abstract class BaseRequestHandler<TRequest, TResponse, TRequestHandler> : IRequestHandler<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
        where TRequestHandler : class
    {
        public abstract Task<TResponse> ProcessRequest(TRequest request);
    }
}
