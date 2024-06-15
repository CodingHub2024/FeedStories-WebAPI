using FeedStories.Common.Utilities;
using FeedStories.Common.Utilities.Type;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FeedStories.Common.Filters
{
    /// <summary>
    /// Action filter for checking the model states and sending bad request if required
    /// </summary>
    public class ValidationModelFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                throw new ApiException(ErrorCodes.BadRequest);
            }
            await next();
        }
    }
}
