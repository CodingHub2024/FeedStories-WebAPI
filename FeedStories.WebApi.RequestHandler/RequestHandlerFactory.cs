namespace FeedStories.WebApi.RequestHandler
{
    /// <summary>
    /// RequestHandlerFactory is used as a factory clas to call respective request handler
    /// </summary>
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public RequestHandlerFactory(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Factory method to create the objects of the request handlers
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TResponse> ProcessRequest<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class
        {
            var handler = (IRequestHandler<TRequest,TResponse>)_serviceProvider.GetService(typeof(IRequestHandler<TRequest, TResponse>))
                ?? throw new NotImplementedException("No handler registered for type:" + typeof(TRequest).FullName);

            return await handler.ProcessRequest(request);
        }
    }
}
