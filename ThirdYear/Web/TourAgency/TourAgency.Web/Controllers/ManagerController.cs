using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Web.Filters;
using TourAgency.Web.Helpers;
using TourAgency.Web.Models;
using TourAgency.Web.Models.Paginations;

namespace TourAgency.Web.Controllers
{
    [Authorize(Roles = "manager, admin")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService manager)
        {
            _managerService = manager;
        }
        public ActionResult Index(MessageViewModel message)
        {
            if (!string.IsNullOrEmpty(message.Status) && !string.IsNullOrEmpty(message.Info))
            {
                ViewData["message"] = message.Info;
                ViewData["status"] = message.Status;
            }
            return View();
        }
        [HttpGet]
        public ActionResult InfoTourSelect(int page = 1)
        {

            int pageSize = 5;
            var activeTours = _managerService.GetActiveTours();
            var activeToursViewModel = MappingViewModel.MapTourListViewModel(activeTours);
            var activeToursPerPages = activeToursViewModel.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = activeToursViewModel.Count };
            var ivm = new TourPaginViewModel { PageInfo = pageInfo, Tours = activeToursPerPages.ToList() };
            return View(ivm);
        }
        [NullExceptionFilter]
        public ActionResult UpdateTour(int id, string startOfTour, string endOfTour,
            int? typeOfTourId, int? typeOfHotelsId, int? maxNumberOfPeople, int? price,
            int? cityId, string isHot, int? numberOfOrders)
        {
            if (Request.HttpMethod == "POST")
            {
                var tour = MappingViewModel.MapTourViewModel(_managerService.GetTourById(id));
                DateTime startOfTourDate = DateTime.MinValue;
                DateTime endOfTourDate = DateTime.MinValue;
                bool update = false;
                if (!string.IsNullOrEmpty(startOfTour) || !string.IsNullOrEmpty(endOfTour))
                {
                    startOfTourDate = Convert.ToDateTime(startOfTour);
                    endOfTourDate = Convert.ToDateTime(endOfTour);
                    if (startOfTourDate > endOfTourDate)
                        ModelState.AddModelError("date", "Enter valid dates");
                    else
                    {
                        tour.StartOfTour = startOfTourDate;
                        tour.StartOfTour = endOfTourDate;
                        update = true;
                    }
                }
                if (ModelState.IsValid)
                {
                    if (typeOfTourId != null)
                    {
                        tour.TypeOfTourId = typeOfTourId.Value;
                        update = true;
                    }
                    if (typeOfHotelsId != null)
                    {
                        tour.TypeOfHotelId = typeOfHotelsId.Value;
                        update = true;
                    }
                    if (maxNumberOfPeople != null)
                    {
                        tour.MaxNumberOfPeople = maxNumberOfPeople.Value;
                        update = true;
                    }
                    if (price != null)
                    {
                        tour.Price = price.Value;
                        update = true;
                    }
                    if (cityId != null)
                    {
                        tour.CityId = cityId.Value;
                        update = true;
                    }
                    if (numberOfOrders != null)
                    {
                        tour.NumberOfOrders = numberOfOrders.Value;
                        update = true;
                    }
                    bool isHotType = Convert.ToBoolean(isHot);
                    if (!((tour.IsHot && isHotType) ||
                        (!tour.IsHot && !isHotType)))
                    {
                        tour.IsHot = Convert.ToBoolean(isHot);
                        update = true;
                    }
                    if (update)
                    {
                        var updateTour = MappingViewModel.MapTourDTO(tour);
                        _managerService.UpdateTour(updateTour);
                        SLogger.InfoToFile($"Manager updated tour id: {updateTour.Id}");
                        var messageInfo = new MessageViewModel()
                        {
                            Status = "success",
                            Info = "Tour updated"
                        };
                        return RedirectToAction("Index", messageInfo);
                    }
                    else
                    {
                        ViewBag.ListOption = _managerService.GetListOption();
                        ModelState.AddModelError("Update", "Tour not updated");
                        return View(tour);
                    }
                }
                else
                {
                    ViewBag.ListOption = _managerService.GetListOption();
                    return View(tour);
                }
            }
            else
            {
                ViewBag.ListOption = _managerService.GetListOption();
                var tour = MappingViewModel.MapTourViewModel(_managerService.GetTourById(id));
                if (tour == null)
                {
                    return HttpNotFound();
                }
                if (tour.NumberOfOrders <= 0)
                {
                    return HttpNotFound();
                }
                return View(tour);
            }
        }
        [NullExceptionFilter]
        public ActionResult UpdateStatus(List<int?> typeOfStatusId)
        {
            var registeredTours = _managerService.GetRegisteredTours();
            var registeredToursViewModel = MappingViewModel.MapTourCustomerListViewModel(registeredTours);
            if (Request.HttpMethod == "POST")
            {
                if (typeOfStatusId != null)
                {
                    for (int i = 0; i < typeOfStatusId.Count; i++)
                    {
                        if (typeOfStatusId[i] != null)
                        {
                            registeredToursViewModel[i].TypeOfStatusId = typeOfStatusId[i].Value;
                            var updateTour = MappingViewModel.MapTourCustomerDTO(registeredToursViewModel[i]);
                            _managerService.UpdateTourCustomer(updateTour);
                            SLogger.InfoToFile($"Manager update status tour id: {updateTour.Id}");
                        }
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ListOption = _managerService.GetListOption();
                return View(registeredToursViewModel);
            }
        }
        public ActionResult CustomerSearch(string fullName, int page = 1)
        {
            int pageSize = 6;
            var customers = _managerService.GetAllCustomers().Where(c =>c.Name+" "+c.Surname  == fullName).ToList();
            var customersViewModel = MappingViewModel.MapCustomerListViewModel(customers);
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = customersViewModel.Count };
            var ivm = new CustomerPaginViewModel { PageInfo = pageInfo, Customers = customersViewModel };
            return View("UpdateCustomerInformation", ivm);
        }
        public ActionResult UpdateCustomerInformation(int page = 1)
        {
            int pageSize = 6;
            var customers = _managerService.GetAllCustomers();
            var customersViewModel = MappingViewModel.MapCustomerListViewModel(customers);
            var customersPerPages = customersViewModel.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = customersViewModel.Count };
            var ivm = new CustomerPaginViewModel { PageInfo = pageInfo, Customers = customersPerPages };
            return View(ivm);
        }
        [ValidationExceptionFilter]
        public ActionResult ChangeDiscount(int id, int? maxDiscount,int? stepDiscount)
        {
            var customer = _managerService.GetCustomerById(id);
            var customerViewModel = MappingViewModel.MapCustomerViewModel(customer);
            if (Request.HttpMethod == "POST")
            {
                if (maxDiscount == null)
                {
                    ModelState.AddModelError("maxDiscount", "Please enter max discount");
                }
                if (stepDiscount == null)
                {
                    ModelState.AddModelError("stepDiscount", "Please enter step discount");
                }
                if (ModelState.IsValid)
                {
                    customer.StepDiscount = stepDiscount.Value;
                    customer.MaxDiscount = maxDiscount.Value;
                    _managerService.ChangeDiscountCustomer(customer);
                    SLogger.InfoToFile($"Manager change discount customer id: {customer.Id}");
                    var messageInfo = new MessageViewModel()
                    {
                        Status = "success",
                        Info = $"Changed discount for {customer.Name}"
                    };
                    return RedirectToAction("Index", messageInfo);
                }
                else
                {
                    return View(customerViewModel);
                }
            }
            else
            {
                return View(customerViewModel);
            }
        }
        [HttpGet]
        [NullExceptionFilter]
        public ActionResult ViewFeedback()
        {
            var feedbacks = _managerService.GetActiveFeedbacks();
            var feedbacksViewModel = MappingViewModel.MapFeedbackListViewModel(feedbacks);
            foreach (var item in feedbacksViewModel)
            {
                item.Message = item.Message.Substring(0, 20);
            }
            return View(feedbacksViewModel);
        }
        [NullExceptionFilter]
        public ActionResult DetailFeedback(int? id)
        {
            var feedback = _managerService.GetActiveFeedbacks().Where(f=>f.Id == id.Value).First();
            var feedbackViewModel = MappingViewModel.MapFeedbackViewModel(feedback);
            if (Request.HttpMethod == "POST")
            {
                _managerService.ReadFeedback(id.Value);
                return RedirectToAction("Index");
            }
            else
            {
                return View(feedbackViewModel);
            }
        }
        protected override void Dispose(bool disposing)
        {
            _managerService.Dispose();
            base.Dispose(disposing);
        }
    }
}