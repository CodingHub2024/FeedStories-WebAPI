using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using Microsoft.AspNetCore.Mvc;

namespace FeedStories.WebApi.Service.Controllers
{
    [Route("[controller]")]
    public class StoryController : BaseController
    {
        public StoryController(IRequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory)
        {
        }

        [HttpGet("GetStoryIds")]
        [ProducesResponseType(typeof(StoryIdResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStoryIds()
        {
            return Ok(await HandlerFactory.ProcessRequest<EmptyRequest,StoryIdResponse>(EmptyRequest.Instance));
        }
    }
}