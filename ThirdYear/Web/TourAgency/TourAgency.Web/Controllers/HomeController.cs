using Microsoft.AspNet.Identity;
using Serilog;
using System;
using System.Web.Hosting;
using System.Web.Mvc;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Web.Helpers;
using TourAgency.Web.Models;

namespace TourAgency.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;
        public HomeController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public ActionResult Index()
        {
            _customerService.GetActiveTours().AsReadOnly();
            if (User.IsInRole("admin"))
            {
                return RedirectPermanent("/Admin/Index");
            }
            else if (User.IsInRole("manager"))
            {
                return RedirectPermanent("/Manager/Index");
            }
            else if (User.IsInRole("customer"))
            {
                return RedirectPermanent("/Customer/Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string message)
        {
            if ( string.IsNullOrEmpty(message) || message.Length < 20)
            {
                ModelState.AddModelError("message", "Too short feedback");
            }
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var customer = _customerService.GetCustomerByIdentityUserId(userId);
                var cistomerViewModel = MappingViewModel.MapCustomerViewModel(customer);
                var feedback = new FeedbackViewModel()
                {
                    Customer = cistomerViewModel,
                    CustomerId = cistomerViewModel.Id,
                    IsRead = false,
                    Message = message,
                    Date = DateTime.Now
                };
                var feedbackDto = MappingViewModel.MapFeedbackDTO(feedback);
                _customerService.SendFeedback(feedbackDto);
                SLogger.InfoToFile($"Customer {customer.Id} sent feedback");
                var messageInfo = new MessageViewModel()
                {
                    Status = "success",
                    Info = "Your feedback has been sent"
                };
                return RedirectToAction("Index","Customer", messageInfo);
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _customerService.Dispose();
            base.Dispose(disposing);
        }
    }
}