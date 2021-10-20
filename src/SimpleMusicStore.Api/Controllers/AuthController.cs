using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Auth;
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

        public Task<string> Google(string token)
        {
            return _authenticator.Google(token);

        }
    }
}