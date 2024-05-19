﻿using FeedStories.WebApi.Contracts.Request;
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


        /// <summary>
        /// GetStoryIds method is used to get story ids
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStoryIds")]
        [ProducesResponseType(typeof(StoryIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStoryIds()
        {
            return Ok(await HandlerFactory.ProcessRequest<EmptyRequest, StoryIdResponse>(EmptyRequest.Instance));
        }

        /// <summary>
        /// GetStoryDetails method is used to get entire story details
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetStoryDetails")]
        [ProducesResponseType(typeof(StoryDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStoryDetails([FromBody] StoryIdRequest request)
        {
            return Ok(await HandlerFactory.ProcessRequest<StoryIdRequest, StoryDetailResponse>(request));
        }
    }
}