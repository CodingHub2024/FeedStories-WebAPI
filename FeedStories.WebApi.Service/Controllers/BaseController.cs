using FeedStories.WebApi.RequestHandler;
using Microsoft.AspNetCore.Mvc;

namespace FeedStories.WebApi.Service.Controllers
{
    public class BaseController : Controller
    {
        protected IRequestHandlerFactory HandlerFactory { get; private set; }
        public BaseController(IRequestHandlerFactory requestHandlerFactory)
        {
            HandlerFactory = requestHandlerFactory;
        }
    }
}
