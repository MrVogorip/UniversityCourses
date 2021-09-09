using System;
using System.Web.Mvc;

namespace TourAgency.Web.Filters
{
    public class NullExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            {
                filterContext.Result = new RedirectResult("/Error.cshtml");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}