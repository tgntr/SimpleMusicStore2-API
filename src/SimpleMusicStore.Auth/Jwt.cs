using Microsoft.Extensions.Options;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Models.AuthenticationProviders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SimpleMusicStore.Auth.Extensions;
using SimpleMusicStore.Contracts.Repositories;
using System.Threading.Tasks;
using System;
using SimpleMusicStore.Entities;

namespace SimpleMusicStore.Auth
{
    public class Jwt : AuthenticationHandler
    {
        private readonly IUserRepository _users;
        private readonly JwtConfiguration _config;

        public Jwt(IUserRepository users, IOptions<JwtConfiguration> config)
        {
            _users = users;
            _config = config.Value;
        }

        public async Task<string> Authenticate(AuthenticationRequest request)
        {
            await CheckIfCredentialsAreValid(request);
            var user = await _users.Find(request.Username);
            return GenerateJwtToken(user);
        }

        private async Task CheckIfCredentialsAreValid(AuthenticationRequest request)
        {
            if (!await _users.IsValid(request))
                //TODO configure so api returns proper error when thrown
                throw new ArgumentException("invalid username or password");
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim("id", user.Id),
                new Claim("username", user.Username)
            };

            var token = _config.SecurityToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
