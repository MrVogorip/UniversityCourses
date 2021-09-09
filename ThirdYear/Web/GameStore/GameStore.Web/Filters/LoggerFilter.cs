using System.Linq;
using GameStore.Domain.Interfaces;
using GameStore.Logging.LoggerExtensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameStore.Web.Filters
{
    public class LoggerFilter : IActionFilter
    {
        private readonly IGameStoreLogger _logger;

        public LoggerFilter(IGameStoreLogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.HttpContext.Request.RouteValues["controller"].ToString();
            var action = context.HttpContext.Request.RouteValues["action"].ToString();

            if (context.HttpContext.Response.StatusCode != 200)
            {
                _logger.LogFilterExecutedError(controller, action);
            }
            else
            {
                _logger.LogFilterExecuted(controller, action);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var queryList = context.ActionArguments.Select(x => string.Join(':', x.Key, x.Value)).ToList();
            var query = queryList.Count != 0 ? string.Join(',', queryList) : "No query";

            var controller = context.HttpContext.Request.RouteValues["controller"].ToString();
            var action = context.HttpContext.Request.RouteValues["action"].ToString();

            if (context.HttpContext.Response.StatusCode != 200)
            {
                _logger.LogFilterExecutingError(query, controller, action);
            }
            else
            {
                _logger.LogFilterExecuting(query, controller, action);
            }
        }
    }
}
