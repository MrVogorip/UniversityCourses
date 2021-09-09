using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourAgency.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
        public ActionResult Teapot()
        {
            Response.StatusCode = 418;
            return View();
        }
        public ActionResult Blocked()
        {
            return View();
        }
    }
}