using Microsoft.AspNetCore.Builder;

namespace FeedStories.Common.Middlewares
{
    /// <summary>
    /// MiddlewareExtensions class is used to register Eception handling middleware in request processinf pipeline
    /// </summary>
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder HandleExceptions(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}