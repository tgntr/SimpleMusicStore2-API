using Microsoft.Extensions.Options;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Models.AuthenticationProviders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SimpleMusicStore.Auth.Extensions;
using SimpleMusicStore.Contracts.Repositories;
using System.Threading.Tasks;
using System;

namespace SimpleMusicStore.Auth
{
    public class Jwt : AuthenticationHandler
    {
        private readonly IUserRepository _users;
        //private readonly IdentityHandler _userManager;
        private readonly JwtConfiguration _config;

        public Jwt(IUserRepository users, IOptions<JwtConfiguration> config)
        {
            _users = users;
            //_userManager = userManager;
            _config = config.Value;
        }

        public async Task<string> Authenticate(AuthenticationRequest request)
		{
            if (!await _users.IsValid(request))
                //TODO configure so api returns proper error when thrown
                throw new ArgumentException("invalid username or password");

			return GenerateJwtToken(request.Username);
		}

		private string GenerateJwtToken(string username)
		{
            var claims = new Claim[]
            {
				new Claim("username", username)
            };

            var token = _config.SecurityToken(claims);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
