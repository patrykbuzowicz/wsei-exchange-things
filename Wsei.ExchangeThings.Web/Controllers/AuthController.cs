using Microsoft.AspNetCore.Mvc;
using Wsei.ExchangeThings.Web.Auth;
using Wsei.ExchangeThings.Web.Models;

namespace Wsei.ExchangeThings.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginModel model, [FromQuery] string returnUrl)
        {
            var authenticationResult = _authService.Authenticate(model.Username, model.Password);

            if (authenticationResult == AuthResult.Success)
            {
                var redirectTo = Url.IsLocalUrl(returnUrl)
                    ? returnUrl
                    : "/";

                return Redirect(redirectTo);
            }

            ViewBag.Failed = true;
            return View();
        }
    }
}
