using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using TourAgency.Bll.BusinessModels;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Web.Filters;
using TourAgency.Web.Helpers;
using TourAgency.Web.Models;
using TourAgency.Web.Models.Paginations;

namespace TourAgency.Web.Controllers
{
    [Authorize(Roles = "customer")]
    public class CustomerController : Controller
    {
        // GET: Customer
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public ActionResult Index(MessageViewModel message)
        {
            if (!string.IsNullOrEmpty(message.Status) && !string.IsNullOrEmpty(message.Info))
            {
                ViewData["message"] = message.Info;
                ViewData["status"] = message.Status;
            }
            var hotTours = _customerService.GetHotAndNewTours();
            var hotToursViewModel = MappingViewModel.MapTourListViewModel(hotTours);
            return View(hotToursViewModel);
        }
        public ActionResult TourCatalog(int? sort, int? sortType, int page = 1)
        {
            var activeTours = _customerService.GetActiveTours();
            var activeToursViewModel = MappingViewModel.MapTourListViewModel(activeTours);

            int pageSize = 6;
            var activeToursPerPages = activeToursViewModel.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = activeToursViewModel.Count };
            var ivm = new TourPaginViewModel { PageInfo = pageInfo, Tours = activeToursPerPages.ToList() };
            if (Request.HttpMethod == "POST")
            {
                if (sort != null && sortType != null)
                {
                    switch (sort.Value)
                    {
                        case 1:
                            {
                                if (sortType.Value == 1)
                                    ivm.Tours.Sort((x, y) => x.Price.CompareTo(y.Price));
                                else
                                    ivm.Tours.Sort((x, y) => y.Price.CompareTo(x.Price));
                                break;
                            }
                        case 2:
                            {
                                if (sortType.Value == 1)
                                    ivm.Tours.Sort((x, y) => x.TypeOfTourId.CompareTo(y.TypeOfTourId));
                                else
                                    ivm.Tours.Sort((x, y) => y.TypeOfTourId.CompareTo(x.TypeOfTourId));
                                break;
                            }
                        case 3:
                            {
                                if (sortType.Value == 1)
                                    ivm.Tours.Sort((x, y) => x.MaxNumberOfPeople.CompareTo(y.MaxNumberOfPeople));
                                else
                                    ivm.Tours.Sort((x, y) => y.MaxNumberOfPeople.CompareTo(x.MaxNumberOfPeople));
                                break;
                            }
                        case 4:
                            {
                                if (sortType.Value == 1)
                                    ivm.Tours.Sort((x, y) => x.TypeOfHotel.NumberOfStars.CompareTo(y.TypeOfHotel.NumberOfStars));
                                else
                                    ivm.Tours.Sort((x, y) => y.TypeOfHotel.NumberOfStars.CompareTo(x.TypeOfHotel.NumberOfStars));
                                break;
                            }
                        case 5:
                            {
                                if (sortType.Value == 1)
                                    ivm.Tours.Sort((x, y) => x.StartOfTour.CompareTo(y.StartOfTour));
                                else
                                    ivm.Tours.Sort((x, y) => y.StartOfTour.CompareTo(x.StartOfTour));
                                break;
                            }
                        default:
                            break;
                    }
                }
                return View(ivm);
            }
            else
            {
                return View(ivm);
            }
        }
        [NullExceptionFilter]
        public ActionResult OrderTour(int id, int? realNumberOfPeople)
        {
            var tour = _customerService.GetTourById(id);
            var tourViewModel = MappingViewModel.MapTourViewModel(tour);
            var userId = User.Identity.GetUserId();
            var customer = _customerService.GetCustomerByIdentityUserId(userId);
            if (Request.HttpMethod == "POST")
            {
                if (realNumberOfPeople == null)
                    ModelState.AddModelError("realNumberOfPeople", "Please enter valid number of people");
                else
                    if (realNumberOfPeople > tourViewModel.MaxNumberOfPeople)
                    ModelState.AddModelError("realNumberOfPeople", "Please enter valid number of people");
                if (ModelState.IsValid)
                {
                    var tourDto = MappingViewModel.MapTourDTO(tourViewModel);
                    int cost = Discount.DiscountPrice(tourViewModel.Price, customer.Discount); 
                    _customerService.BuyTour(tourDto, userId, realNumberOfPeople.Value, cost) ;
                    SLogger.InfoToFile($"Customer {customer.Id} order tour {tourViewModel.Id} by price {cost}");
                    var messageInfo = new MessageViewModel()
                    {
                        Status = "success",
                        Info = "Tour ordered"
                    };
                    return RedirectToAction("Index", messageInfo);
                }
                else
                {
                    ViewBag.PriceForCustomer = Discount.DiscountPrice(tourViewModel.Price, customer.Discount);
                    return View(tourViewModel);
                }
            }
            else
            {
                ViewBag.PriceForCustomer = Discount.DiscountPrice(tourViewModel.Price, customer.Discount);
                return View(tourViewModel);
            }
        }
        public ActionResult ListMyTours()
        {
            var userId = User.Identity.GetUserId();
            var tours = _customerService.GetTourCustomerByCustomerId(userId);
            var toursViewModel = MappingViewModel.MapTourCustomerListViewModel(tours);
            return View(toursViewModel);
        }
        [NullExceptionFilter]
        public ActionResult PersonalArea()
        {
            var userId = User.Identity.GetUserId();
            var tours = _customerService.GetTourCustomerByCustomerId(userId);
            var customer = _customerService.GetCustomerByIdentityUserId(userId);
            var cistomerViewModel = MappingViewModel.MapCustomerViewModel(customer);
            var toursViewModel = MappingViewModel.MapTourCustomerListViewModel(tours);
            ViewBag.Customer = cistomerViewModel;
            return View(toursViewModel);
        }
        [NullExceptionFilter]
        public ActionResult ChangeInformation(string name, string surname)
        {
            var userId = User.Identity.GetUserId();
            var customer = _customerService.GetCustomerByIdentityUserId(userId);
            var cistomerViewModel = MappingViewModel.MapCustomerViewModel(customer);
            if (Request.HttpMethod == "POST")
            {
                if (name.Length <= 3)
                    ModelState.AddModelError("name", "Please enter valid name");
                if (surname.Length <= 3)
                    ModelState.AddModelError("surname", "Please enter valid surname");
                if (ModelState.IsValid)
                {
                    customer.Name = name;
                    customer.Surname = surname;
                    _customerService.ChangePersonalInformation(customer);
                    SLogger.InfoToFile($"Customer {customer.Id} changed name to {name} {surname}");
                    var messageInfo = new MessageViewModel()
                    {
                        Status = "success",
                        Info = "Changed information"
                    };
                    return RedirectToAction("Index", messageInfo);
                }
                else
                {
                    return View(cistomerViewModel);
                }
            }
            else
            {
                return View(cistomerViewModel);
            }
        }
        [ValidationExceptionFilter]
        public ActionResult TourCancellation(int? id)
        {
            var userId = User.Identity.GetUserId();
            var customer = _customerService.GetCustomerByIdentityUserId(userId);
            var tours = _customerService.GetTourCustomerByCustomerId(userId);
            var toursViewModel = MappingViewModel.MapTourCustomerListViewModel(tours);
            var tourCustomerViewModel = toursViewModel.Find(t => t.Id == id.Value);
            var tourCustomer = MappingViewModel.MapTourCustomerDTO(tourCustomerViewModel);
            _customerService.CancelTour(tourCustomer);
            SLogger.InfoToFile($"Customer {customer.Id} Canceled the tour {tourCustomer.Id}");
            var messageInfo = new MessageViewModel()
            {
                Status = "success",
                Info = "Tour canceled -5 percent of the discount"
            };
            return RedirectToAction("Index", messageInfo);
        }
        protected override void Dispose(bool disposing)
        {
            _customerService.Dispose();
            base.Dispose(disposing);
        }
    }
}