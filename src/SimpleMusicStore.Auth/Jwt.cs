using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleMusicStore.Auth
{
    public class Jwt : AuthenticationHandler
    {
        private readonly IdentityHandler _userManager;
        private readonly JwtTokenConfiguration _token;
        public Jwt(IdentityHandler userManager, IOptions<JwtTokenConfiguration> token)
        {
            _userManager = userManager;
            _token = token.Value;
        }
        public bool TryAuthenticate(AuthenticationRequest request, out string token)
		{
			token = string.Empty;
			if (!_userManager.IsValidUser(request))
			{
				return false;
			}
			token = GenerateJwtToken(request.Username);
			return true;
		}

		//TODO claims
		private string GenerateJwtToken(string username)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, username)
			};
			var secret = new SymmetricSecurityKey(_token.SecretEncoded());
			var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
			var jwtToken = new JwtSecurityToken(
				issuer:_token.Issuer,
				audience: _token.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_token.AccessExpiration),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(jwtToken);
		}
	}
}
