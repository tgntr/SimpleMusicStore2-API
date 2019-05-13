using Microsoft.Extensions.Options;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Models.AuthenticationProviders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SimpleMusicStore.Auth.Extensions;

namespace SimpleMusicStore.Auth
{
    public class Jwt : AuthenticationHandler
    {
        private readonly IdentityHandler _userManager;
        private readonly JwtConfiguration _config;

        public Jwt(IdentityHandler userManager, IOptions<JwtConfiguration> config)
        {
            _userManager = userManager;
            _config = config.Value;
			//TODO validate the jwtConfiguration?
        }

        public bool TryAuthenticate(AuthenticationRequest request, out string token)
		{
			token = string.Empty;
			if (!_userManager.Exists(request))
			{
				return false;
			}
			token = GenerateJwtToken(request.Username);
			return true;
		}

		private string GenerateJwtToken(string username)
		{
            var claims = new Claim[]
            {
				//TODO requiring these claims returns 403 forbidden
				new Claim("karizma", username)
            };

            var token = _config.SecurityToken(claims);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
