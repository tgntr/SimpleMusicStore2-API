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

        [Authorize]
		public Task Login()
		{
            return _users.Add(User);
		}
    }
}