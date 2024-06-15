using FeedStories.Common.TestData;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using FeedStories.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;

namespace FeedStories.WebApi.UnitTests.Controller
{
    [TestClass]
    public class StoryControllerUnitTests
    {
        private readonly IRequestHandlerFactory requestHandlerFactory =  Substitute.For<IRequestHandlerFactory>();

        [TestMethod]
        public void GetStoryIds_Returns_Content()
        {
            //Arrange
            requestHandlerFactory.ProcessRequest<StoryIdRequest, StoryIdResponse>(Arg.Any<StoryIdRequest>()).Returns(StoryTestData.StoryIdResponse);
            var storyController = new StoryController(requestHandlerFactory);

            //Act
            var result = storyController.GetStoryIds(StoryTestData.StoryIdRequest).Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreEqual(result.Value, StoryTestData.StoryIdResponse);
        }

        [TestMethod]
        public void GetStoryIds_Throws_Exception()
        {
            //Arrange
            requestHandlerFactory.ProcessRequest<StoryIdRequest, StoryIdResponse>(Arg.Any<StoryIdRequest>()).ThrowsAsync<Exception>();
            var storyController = new StoryController(requestHandlerFactory);

            //Act
            var task = storyController.GetStoryIds(StoryTestData.StoryIdRequest);

            //Assert
            Assert.IsNotNull(task.Exception);
        }
    }
}
