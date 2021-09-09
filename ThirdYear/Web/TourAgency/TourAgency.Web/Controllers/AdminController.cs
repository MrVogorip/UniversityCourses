using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Web.Filters;
using TourAgency.Web.Helpers;
using TourAgency.Web.Models;

namespace TourAgency.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService administrator)
        {
            _adminService = administrator;
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
        [NullExceptionFilter]
        public ActionResult CreateNewTour(string startOfTour, string endOfTour,
        int? typeOfTourId, int? typeOfHotelsId, int? maxNumberOfPeople, int? price, 
        int? cityId, int? numberOfOrders, HttpPostedFileBase upload)
        {
            if (Request.HttpMethod == "POST")
            {
                DateTime startOfTourDate = DateTime.MinValue;
                DateTime endOfTourDate = DateTime.MinValue;
                if (string.IsNullOrEmpty(startOfTour) || string.IsNullOrEmpty(endOfTour))
                    ModelState.AddModelError("date", "Enter valid dates");
                else
                {
                    startOfTourDate = Convert.ToDateTime(startOfTour);
                    endOfTourDate = Convert.ToDateTime(endOfTour);
                    if (startOfTourDate > endOfTourDate)
                        ModelState.AddModelError("date", "Enter valid dates");
                }
                if (upload == null)
                    ModelState.AddModelError("upload", "Please enter image");
                if (typeOfTourId == null)
                    ModelState.AddModelError("typeOfTourId", "Please enter type of tour");
                if (typeOfHotelsId == null)
                    ModelState.AddModelError("typeOfHotelsId", "Please enter type of hotel");
                if (maxNumberOfPeople == null)
                    ModelState.AddModelError("maxNumberOfPeople", "Please enter max number of people");
                if (price == null)
                    ModelState.AddModelError("price", "Please enter price");
                if (cityId == null)
                    ModelState.AddModelError("cityId", "Please enter a city");
                if (numberOfOrders == null)
                    ModelState.AddModelError("typeOfTourId", "Please enter number of orders");
                if (ModelState.IsValid)
                {
                    var newTour = new TourViewModel()
                    {
                        StartOfTour = startOfTourDate,
                        EndOfTour = endOfTourDate,
                        TypeOfHotelId = typeOfHotelsId.Value,
                        TypeOfTourId = typeOfTourId.Value,
                        MaxNumberOfPeople = maxNumberOfPeople.Value,
                        IsDelete = false,
                        NumberOfOrders = numberOfOrders.Value,
                        Price = price.Value,
                        CityId = cityId.Value,
                        IsHot = false,
                        ImagePath = Path.GetFileName(upload.FileName)
                    };
                    upload.SaveAs(Server.MapPath("~/Content/Image/" + newTour.ImagePath));
                    var newTourDTO = MappingViewModel.MapTourDTO(newTour);
                    _adminService.CreateTour(newTourDTO);
                    SLogger.InfoToFile($"Admin create a new tour");
                    var messageInfo = new MessageViewModel()
                    {
                        Status = "success",
                        Info = "Tour created"
                    };
                    return RedirectToAction("Index", messageInfo);
                }
                else
                {
                    var listOption = _adminService.GetListOption();
                    return View(listOption);
                }
            }
            else
            {
                var listOption = _adminService.GetListOption();
                return View(listOption);
            }
        }
        [ValidationExceptionFilter]
        public ActionResult DeleteTour(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                _adminService.DeleteTour(id);
                SLogger.InfoToFile($"Admin delete tour id: {id}");
                var messageInfo = new MessageViewModel()
                {
                    Status = "success",
                    Info = "Tour deleted"
                };
                return RedirectToAction("Index", messageInfo);
            }
            else
            {
                var tour = MappingViewModel.MapTourViewModel(_adminService.GetTourById(id));
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
        [ValidationExceptionFilter]
        public ActionResult BlockUnlockCustomer(int? id)
        {
            if (id != null)
            {
                var customer = _adminService.GetCustomerById(id.Value);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                if (customer.IsBlock)
                {
                    _adminService.UnlockCustomer(id.Value);
                    SLogger.InfoToFile($"Admin unlock customer id: {id.Value}");
                }
                else
                {
                    _adminService.BlockCustomer(id.Value);
                    SLogger.InfoToFile($"Admin block customer id: {id.Value}");
                }
                return RedirectToAction("Index", "Home");

            }
            var customers = _adminService.GetAllCustomers();
            var customersViewModel = MappingViewModel.MapCustomerListViewModel(customers);
            return View(customersViewModel);
        }
        [ValidationExceptionFilter]
        public ActionResult BlockUnlockManager(int? id)
        {
            var managers = _adminService.GetAllManagers();
            var managersViewModel = MappingViewModel.MapManagerListViewModel(managers);
            if (id != null)
            {
                var manager = _adminService.GetManagerById(id.Value);
                if (manager == null)
                {
                    return HttpNotFound();
                }
                if (manager.IsBlock)
                {
                    _adminService.UnlockManager(id.Value);
                    SLogger.InfoToFile($"Admin unlock manager id: {id.Value}");
                }
                else
                {
                    _adminService.BlockManager(id.Value);
                    SLogger.InfoToFile($"Admin block manager id: {id.Value}");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(managersViewModel);
            }

        }
        public ActionResult AddHotel(TypeOfHotelViewModel typeOfHotel)
        {
            if (Request.HttpMethod == "POST")
            {
                var typeOfHotelDto = MappingViewModel.MapTypeOfHotelDTO(typeOfHotel);
                _adminService.AddHotel(typeOfHotelDto);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AddTypeOfTour(TypeOfTourViewModel typeOfTour)
        {
            if (Request.HttpMethod == "POST")
            {
                var typeOfTourDto = MappingViewModel.MapTypeOfTourDTO(typeOfTour);
                _adminService.AddTypeOfTour(typeOfTourDto);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AddCity(CityViewModel city)
        {
            if (Request.HttpMethod == "POST")
            {
                var cityDto = MappingViewModel.MapCityDTO(city);
                _adminService.AddCity(cityDto);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        protected override void Dispose(bool disposing)
        {
            _adminService.Dispose();
            base.Dispose(disposing);
        }
    }
}