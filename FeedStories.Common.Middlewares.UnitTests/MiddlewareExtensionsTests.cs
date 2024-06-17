using Microsoft.AspNetCore.Builder;
using NSubstitute;

namespace FeedStories.Common.Middlewares.UnitTests
{
    [TestClass]
    public class MiddlewareExtensionsTests
    {
        private readonly IApplicationBuilder appBuilder = Substitute.For<IApplicationBuilder>();

        [TestMethod]
        public void HandleExceptions_ShouldAddExceptionHandlerMiddleware()
        {
            // Act
            var result = appBuilder.HandleExceptions();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
