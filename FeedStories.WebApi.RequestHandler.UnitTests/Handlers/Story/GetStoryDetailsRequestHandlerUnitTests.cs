using FeedStories.Common.TestData;
using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.RequestHandler.Handlers;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FeedStories.WebApi.RequestHandler.UnitTests
{
    [TestClass]
    public class GetStoryDetailsRequestHandlerUnitTests
    {
        private readonly ILogger<GetStoryDetailsRequestHandler> _logger = Substitute.For<ILogger<GetStoryDetailsRequestHandler>>();
        private readonly IStoryService _storyService = Substitute.For<IStoryService>();


        [TestMethod]
        public void ProcessRequest_Returns_StoryDetailsLis()
        {
            //Arrange
            _storyService.GetStoryDetails(Arg.Any<int>()).Returns(StoryTestData.storyDetailResponse);
            var getStoryDetailsRequestHandler = new GetStoryDetailsRequestHandler(_logger, _storyService);
            
            //Act
            var task = getStoryDetailsRequestHandler.ProcessRequest(StoryTestData.storyDetailRequest);

            //Assert
            Assert.AreEqual(StoryTestData.storyDetailResponse, task.Result);
        }
    }
}
