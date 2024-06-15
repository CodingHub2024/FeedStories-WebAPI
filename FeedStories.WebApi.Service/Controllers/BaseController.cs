using FeedStories.WebApi.RequestHandler;
using Microsoft.AspNetCore.Mvc;

namespace FeedStories.WebApi.Controllers
{
    /// <summary>
    /// Basecontroller class for all other controllers
    /// </summary>
    public class BaseController : Controller
    {
        protected IRequestHandlerFactory HandlerFactory { get; private set; }
        public BaseController(IRequestHandlerFactory requestHandlerFactory)
        {
            HandlerFactory = requestHandlerFactory;
        }
    }
}
