using FeedStories.Common.TestData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net;

namespace FeedStories.Common.Middlewares.UnitTests
{
    [TestClass]
    public class ExceptionHandlerMiddlewareTests
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger = Substitute.For<ILogger<ExceptionHandlerMiddleware>>();
        private readonly RequestDelegate _next = Substitute.For<RequestDelegate>();
        private readonly DefaultHttpContext _context = new DefaultHttpContext();

        [TestMethod]
        public async Task Invoke_ShouldCallNextDelegate_WhenNoExceptionThrown()
        {
            // Arrange
            var middleware = new ExceptionHandlerMiddleware(_next, _logger);

            // Act
            await middleware.Invoke(_context);

            // Assert
            await _next.Received(1)(_context);
        }

        [TestMethod]
        public async Task Invoke_ShouldHandleExceptionAndSetResponse_WhenExceptionThrown()
        {
            // Arrange
            _next.When(n => n.Invoke(_context)).Do(x => throw StoryTestData.TestException);
            // Set up the response body stream
            _context.Response.Body = StoryTestData.StoryResponseBodyStream;
            var middleware = new ExceptionHandlerMiddleware(_next, _logger);

            // Act
            await middleware.Invoke(_context);

            // Assert
            _logger.Received(1).LogError(StoryTestData.TestException, StoryTestData.TestException.Message);
            
            // Reset the response body stream position to read the response content
            StoryTestData.StoryResponseBodyStream.Seek(0, SeekOrigin.Begin);
            var result = new StreamReader(StoryTestData.StoryResponseBodyStream).ReadToEnd();

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, _context.Response.StatusCode);
            Assert.AreEqual("application/json", _context.Response.ContentType);
            Assert.AreEqual(StoryTestData.StoryExpectedResponse, result);
        }
    }
}