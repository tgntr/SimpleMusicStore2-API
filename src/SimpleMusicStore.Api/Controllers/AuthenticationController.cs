using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Models.AuthenticationProviders;

namespace SimpleMusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationHandler _authenticator;

        public AuthenticationController(AuthenticationHandler authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpPost]
        public ActionResult Post([FromBody] AuthenticationRequest request)
        {
            if (!_authenticator.TryAuthenticate(request, out string token))
            {
                return BadRequest("invalid username or password");
            }

            return Ok(token);
        }
    }
}