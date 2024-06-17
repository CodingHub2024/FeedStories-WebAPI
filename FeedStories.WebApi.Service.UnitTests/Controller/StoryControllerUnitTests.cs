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
        public void GetStories_Should_Return_Content()
        {
            //Arrange
            requestHandlerFactory.ProcessRequest<StoryRequest, StoryResponse>(Arg.Any<StoryRequest>()).Returns(StoryTestData.StoryResponse);
            var storyController = new StoryController(requestHandlerFactory);

            //Act
            var result = storyController.GetStories(StoryTestData.StoryIdRequest).Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreEqual(result.Value, StoryTestData.StoryResponse);
        }

        [TestMethod]
        public void GetStories_Should_Throw_Exception()
        {
            //Arrange
            requestHandlerFactory.ProcessRequest<StoryRequest, StoryResponse>(Arg.Any<StoryRequest>()).ThrowsAsync<Exception>();
            var storyController = new StoryController(requestHandlerFactory);

            //Act
            var task = storyController.GetStories(StoryTestData.StoryIdRequest);

            //Assert
            Assert.IsNotNull(task.Exception);
        }
    }
}
