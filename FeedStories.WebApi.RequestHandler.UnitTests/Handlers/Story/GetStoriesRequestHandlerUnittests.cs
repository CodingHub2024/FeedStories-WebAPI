using FeedStories.Common.TestData;
using FeedStories.Common.Utilities.Interface;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FeedStories.WebApi.RequestHandler.UnitTests
{
    [TestClass]
    public class GetStoriesRequestHandlerUnittests
    {
        private readonly ILogger<Handlers.GetStoriesRequestHandler> _logger = Substitute.For<ILogger<Handlers.GetStoriesRequestHandler>>();
        private readonly IStoryService _storyService = Substitute.For<IStoryService>();


        //[TestMethod]
        //public void ProcessRequest_Returns_StoryDetailsLis()
        //{
        //    //Arrange
        //    _storyService.GetStoryDetails(Arg.Any<int>()).Returns(StoryTestData.StoryDetailResponse);
        //    var getStoryDetailsRequestHandler = new Handlers.GetStoriesRequestHandler(_logger, _storyService);
            
        //    //Act
        //    var task = getStoryDetailsRequestHandler.ProcessRequest(StoryTestData.StoriesResponse);

        //    //Assert
        //    Assert.AreEqual(StoryTestData.StoryDetailResponse, task.Result);
        //}
    }
}
