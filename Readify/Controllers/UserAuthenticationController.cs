using Readify.Models.Authentication;
using Readify.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Readify.Controllers
{
    [AllowAnonymous]
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        public UserAuthenticationController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        #region Registration
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(Registration registration)
        {
            if (!ModelState.IsValid)
                return View(registration);
            

            registration.strRole = "User";
            var result = await _userAuthenticationService.RegistrationAsync(registration);
            TempData["Message"] = result.strMessage;
            TempData["Status"] = result.intStatusCode == 1 ? "success" : "danger";

            return RedirectToAction(nameof(Registration));
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
                return View(login);
            

            var result = await _userAuthenticationService.LoginAsync(login);

            if(result.intStatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = result.strMessage;
                TempData["Status"] = result.intStatusCode == 1 ? "success" : "danger";
                return View(nameof(Login));
            }
        }
        #endregion

        #region Logout
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userAuthenticationService.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
