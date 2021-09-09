using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult SetStatusCodeNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            return View();
        }

        public ActionResult SetStatusCodeBadRequest()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return View();
        }

        [Route("/Error/{code}")]
        public ActionResult SetStatusCode(int code)
        {
            Response.StatusCode = code;

            return View();
        }
    }
}
