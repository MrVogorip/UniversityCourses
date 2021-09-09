using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Web.Helpers;
using GameStore.Web.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("/ban/{userId}")]
        public ActionResult ChoiceBanDuration(string userId)
        {
            if (AssertUserExists(userId))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var user = _userService.GetById(userId);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            var banUserViewModel = _mapper.Map<BanUserViewModel>(userViewModel);

            return View(banUserViewModel);
        }

        [HttpGet]
        public ActionResult SetBanned(BanUserViewModel banUserViewModel)
        {
            if (AssertUserExists(banUserViewModel.Id))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var user = _userService.GetById(banUserViewModel.Id);
            _userService.SetBanned(user, banUserViewModel.BanDuration);

            return Redirect(ResourceNames.Urls.Games);
        }

        [HttpPost]
        public ActionResult SetUnbanned(string userId)
        {
            if (AssertUserExists(userId))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var user = _userService.GetById(userId);
            _userService.SetUnbanned(user);

            return Redirect(ResourceNames.Urls.Games);
        }

        private bool AssertUserExists(string userId)
        {
            return string.IsNullOrEmpty(userId) || !_userService.IsExist(userId);
        }
    }
}
