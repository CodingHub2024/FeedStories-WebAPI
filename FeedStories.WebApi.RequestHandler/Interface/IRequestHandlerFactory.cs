namespace FeedStories.WebApi.RequestHandler
{
    public interface IRequestHandlerFactory
    {
        Task<TResponse> ProcessRequest<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class;
    }
}
