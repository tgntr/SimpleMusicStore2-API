using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationProviders
{
    public class Jwt : AuthenticationProvider
    {
        private readonly UserManager _userManager;
        private readonly JwtToken _token;
        public Jwt(UserManager userManager, IOptions<JwtToken> token)
        {
            _userManager = userManager;
            _token = token.Value;
        }
        public bool Authenticate(AuthenticationRequest request, out string token)
        {
            token = string.Empty;
            if (!_userManager.IsValidUser(request))
            {
                return false;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            var secret = new SymmetricSecurityKey(_token.SecretEncoded());
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _token.Issuer,
                _token.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_token.AccessExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
