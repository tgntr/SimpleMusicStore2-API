using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Models.Auth;
using System.Threading.Tasks;

namespace SimpleMusicStore.Api.Controllers
{
    public class AuthController : Controller
    {
		private readonly AuthenticationHandler _authenticator;
		
		public AuthController(AuthenticationHandler authenticator)
			: base()
		{
			_authenticator = authenticator;
		}

		[HttpPost]
		public async Task<string> Login([FromBody] AuthenticationRequest credentials)
		{
            return await _authenticator.Authenticate(credentials);
		}
	}
}