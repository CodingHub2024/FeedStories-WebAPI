using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using Microsoft.AspNetCore.Mvc;

namespace FeedStories.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class StoryController : BaseController
    {
        public StoryController(IRequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory) {}

        /// <summary>
        /// GetStoryIds method is used to get story ids
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetStories")]
        [ProducesResponseType(typeof(StoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStories([FromBody] StoryRequest request)
        {
            return Ok(await HandlerFactory.ProcessRequest<StoryRequest, StoryResponse>(request));
        }

        [HttpGet("GetDetails")]
        public IActionResult GetDetails()
        {
            return Ok("anurag");
        }


    }
}