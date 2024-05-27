using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using Microsoft.AspNetCore.Mvc;

namespace FeedStories.WebApi.Service.Controllers
{
    [Route("api/[controller]")]
    public class StoryController : BaseController
    {
        public StoryController(IRequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory)
        {
        }


        /// <summary>
        /// GetStoryIds method is used to get story ids
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetStoryIds")]
        [ProducesResponseType(typeof(StoryIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStoryIds([FromBody] StoryIdRequest request)
        {
            return Ok(await HandlerFactory.ProcessRequest<StoryIdRequest, StoryIdResponse>(request));
        }

        /// <summary>
        /// GetStoryDetails method is used to get entire story details
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetStoryDetails")]
        [ProducesResponseType(typeof(StoryDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStoryDetails([FromBody] StoryDetailRequest request)
        {
            return Ok(await HandlerFactory.ProcessRequest<StoryDetailRequest, StoryDetailResponse>(request));
        }
    }
}