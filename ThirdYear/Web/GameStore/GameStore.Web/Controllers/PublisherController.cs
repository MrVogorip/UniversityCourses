using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Helpers;
using GameStore.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/publisher/{publisherName}")]
        public ActionResult GetPublisherDetails(string publisherName)
        {
            if (!_publisherService.IsExist(publisherName))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var publisher = _publisherService.GetByName(publisherName);
            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);

            return View(publisherViewModel);
        }

        [HttpGet]
        [Route("/publisher/new")]
        public ActionResult CreatePublisher()
        {
            var publisherViewModel = new PublisherViewModel();

            return View(publisherViewModel);
        }

        [HttpPost]
        [Route("/publisher/new")]
        public ActionResult CreatePublisher(PublisherViewModel publisherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(publisherViewModel);
            }

            var publisher = _mapper.Map<Publisher>(publisherViewModel);
            _publisherService.Create(publisher);

            return Redirect($"~/publisher/{publisher.CompanyName}");
        }
    }
}
