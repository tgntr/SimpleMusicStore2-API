using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Models.AuthenticationProviders;

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