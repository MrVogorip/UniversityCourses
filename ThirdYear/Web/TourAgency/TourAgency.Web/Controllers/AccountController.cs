using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Web.Models;
using TourAgency.Web.Helpers;

namespace TourAgency.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IManagerService _managerService;
        private readonly ICustomerService _customerService;

        public AccountController()
        {
        }
        public AccountController(IManagerService manager, ICustomerService customer)
        {
            _managerService = manager;
            _customerService = customer;
        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        string userId = SignInManager.AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId();
                        var customer = _customerService.GetCustomerByIdentityUserId(userId);
                        if (customer != null) {
                            if (customer.IsBlock)
                            {
                                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                return RedirectPermanent("/Error/Blocked");
                            }
                        }
                        else
                        {
                            var manager = _managerService.GetManagerByIdentityUserId(userId);
                            if(manager != null)
                            {
                                if (manager.IsBlock)
                                {
                                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                    return RedirectPermanent("/Error/Blocked");
                                }
                            }
                        }
                        return RedirectToLocal(returnUrl);
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateNewManager()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewManager(RegisterViewModel model, string yourName, string yourSurname)
        {
            if (Request.HttpMethod == "POST")
            {
                if (string.IsNullOrEmpty(yourName) || string.IsNullOrEmpty(yourSurname)
                || yourName.Length < 3 || yourSurname.Length < 3)
                    ModelState.AddModelError("strings", "Enter valid Name and Surname");
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = UserManager.Create(user, model.Password);
                    if (result.Succeeded)
                    {
                        // Add role
                        UserManager.AddToRole(user.Id, "manager");
                        // Create manager
                        var managerViewModel = new ManagerViewModel()
                        {
                            UserId = user.Id,
                            Name = yourName,
                            Surname = yourSurname,
                        };
                        var managerDto = MappingViewModel.MapManagerDTO(managerViewModel);
                        _managerService.RegisterManager(managerDto);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string yourName, string yourSurname)
        {
            if (string.IsNullOrEmpty(yourName) || string.IsNullOrEmpty(yourSurname)
                || yourName.Length < 3 || yourSurname.Length < 3)
                ModelState.AddModelError("strings", "Enter valid Name and Surname");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add role
                    UserManager.AddToRole(user.Id, "customer");
                    // Create customer
                    var customerViewModel = new CustomerViewModel()
                    {
                        UserId = user.Id,
                        Discount = 0,
                        MaxDiscount = 15,
                        StepDiscount = 2,
                        IsBlock = false,
                        Name = yourName,
                        Surname = yourSurname,
                    };
                    var customerDto = MappingViewModel.MapCustomerDTO(customerViewModel);
                    _customerService.Register(customerDto);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }
        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var resultRe = await UserManager.CreateAsync(user);
                if (resultRe.Succeeded)
                {
                    // Add role
                    UserManager.AddToRole(user.Id, "customer");
                    // Create customer
                    var customerViewModel = new CustomerViewModel()
                    {
                        UserId = user.Id,
                        Discount = 0,
                        MaxDiscount = 15,
                        StepDiscount = 2,
                        IsBlock = false,
                        Name = "",
                        Surname = "",
                    };
                    var customerDto = MappingViewModel.MapCustomerDTO(customerViewModel);
                    _customerService.Register(customerDto);
                }
                if (resultRe.Succeeded)
                {
                    resultRe = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (resultRe.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(resultRe);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}