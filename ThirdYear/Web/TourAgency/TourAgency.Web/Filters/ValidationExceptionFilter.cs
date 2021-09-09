using System.Web.Mvc;
using TourAgency.Bll.Infrastructure;

namespace TourAgency.Web.Filters
{
    public class ValidationExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is ValidationException)
            {
                filterContext.Result = new RedirectResult("/Error.cshtml");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}