﻿using FeedStories.Common.Utilities.Interface;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace FeedStories.WebApi.RequestHandler.Handlers
{
    /// <summary>
    /// Request handler for getting feed stories for current requested page
    /// </summary>
    public class GetStoriesRequestHandler : BaseRequestHandler<StoryRequest, StoryResponse, GetStoriesRequestHandler>
    {
        private readonly IStoryService _storyService;
        public GetStoriesRequestHandler(ILogger<GetStoriesRequestHandler> logger, IStoryService storyService) : base(logger)
        {
            _storyService = storyService;
        }

        public override async Task<StoryResponse?> ProcessRequest(StoryRequest request)
        {
            _logger.LogDebug($"Called {nameof(GetStoriesRequestHandler)} ProcessRequest");

            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;
            var filteredStories = new List<StoryDetailResponse?>();

            var StoryIds = (await _storyService.GetStoryIds()).Take(200);

            while (filteredStories.Count < pageSize)
            {
                var storyIdsToFetch = StoryIds.Skip(pageNumber * pageSize).Take(pageSize);

                if (storyIdsToFetch.Count() == 0)
                {
                    // No more items to fetch
                    break;
                }

                // Fetch details concurrently
                var storyTasks = storyIdsToFetch.Select(storyId => GetStoryDetails(storyId));
                var stories = await Task.WhenAll(storyTasks);

                // Filter out null results and add to the final list
                filteredStories.AddRange(stories.Where(details => details != null));

                // Move to the next page
                pageNumber++;
            }

            return new StoryResponse
            {
                // Wait for all tasks to complete
                Stories = filteredStories.Take(pageSize),
                TotalElements = StoryIds.Count()
            };

        }

        /// <summary>
        /// Get story details by story id
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        private async Task<StoryDetailResponse?> GetStoryDetails(int storyId)
        {
            var response = await _storyService.GetStoryDetails(storyId);

            if (response != null)
            {
                response.StoryId = storyId;
            }

            return response;
        }
    }
}