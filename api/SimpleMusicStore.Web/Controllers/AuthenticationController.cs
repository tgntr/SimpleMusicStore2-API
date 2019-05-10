using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationProviders;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Models.Authentication;

namespace SimpleMusicStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationProvider _authenticator;

        public AuthenticationController(AuthenticationProvider authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpPost]
        public ActionResult Post([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_authenticator.Authenticate(request, out string token))
            {
                return BadRequest("invalid username or password");
            }

            return Ok(token);
        }
    }
}