using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using System.Threading.Tasks;

namespace SimpleMusicStore.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _users;

        public AuthController(IUserService users)
			: base()
		{
            _users = users;
        }

        public IActionResult Google()
        {
            return GoogleAuthenticationPage();
        }

        [Authorize]
		public Task Login(string returnUrl)
		{
            //todo in the front end return to returnUrl
            return _users.Add(User);
		}

        public Task Logout(string returnUrl)
        {
            return HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        private IActionResult GoogleAuthenticationPage()
        {
            string returnUrl = HttpContext.Request.Query["ReturnUrl"];
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
            var callbackUrl = Url.Action("Login", "Auth", new { returnUrl });
            return Challenge(new AuthenticationProperties() { RedirectUri = callbackUrl }, "Google");
        }
    }
}