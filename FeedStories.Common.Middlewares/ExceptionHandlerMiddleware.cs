using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FeedStories.Common.Utilities;

namespace FeedStories.Common.Middlewares
{
    /// <summary>
    /// ExceptionHandlerMiddleware is used to handle eceptions globally
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException exception)
            {
                _logger.LogError(exception, $"ApiException Error {exception.Message}");

                var result = JsonSerializer.Serialize(new
                {
                    ErrorCode = exception.ErrorCode,
                    ErrorMessage = exception.ErrorMessage,
                });

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await SendResponse(context, result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                var result = JsonSerializer.Serialize(new
                {
                    ErrorMessage = "An internal server error has occured",
                });

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await SendResponse(context, result);
            }
        }

        private static async Task SendResponse(HttpContext context, string result)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}