using FeedStories.Common.TestData;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using FeedStories.WebApi.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace FeedStories.WebApi.Service.UnitTests.Controller
{
    [TestClass]
    internal class StoryControllerUnitTests
    {
        private readonly IRequestHandlerFactory requestHandlerFactory =  Substitute.For<IRequestHandlerFactory>();

        [TestMethod]
        public void GetStoryIds_Returns_Content()
        {
            //Arrange
            requestHandlerFactory.ProcessRequest<StoryIdRequest, StoryIdResponse>(Arg.Any<StoryIdRequest>()).Returns(StoryTestData.storyIdResponse);
            var storyController = new StoryController(requestHandlerFactory);

            //Act
            var result = storyController.GetStoryIds(StoryTestData.storyIdRequest).Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, StoryTestData.storyIdResponse);
        }
    }
}
