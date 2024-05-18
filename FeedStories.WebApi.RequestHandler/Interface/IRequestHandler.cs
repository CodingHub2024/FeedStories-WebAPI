namespace FeedStories.WebApi.RequestHandler
{
    public  interface IRequestHandler<TRequest,TResponse>
        where TRequest:class
        where TResponse:class
    {
        /// <summary>
        /// Method to be called from controller directly
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> ProcessRequest(TRequest request);
    }
}
