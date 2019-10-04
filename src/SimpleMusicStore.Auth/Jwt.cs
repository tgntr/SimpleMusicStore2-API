using Microsoft.Extensions.Options;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SimpleMusicStore.Auth.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using Google.Apis.Auth;
using System;
using SimpleMusicStore.Models;
using System.Collections.Generic;

namespace SimpleMusicStore.Auth
{
    public class Jwt : AuthenticationHandler
    {
        private readonly JwtConfiguration _config;
        private readonly IUnitOfWork _db;

        public Jwt(IOptions<JwtConfiguration> config, IUnitOfWork db)
        {
            _config = config.Value;
            _db = db;
        }

        public async Task<string> Google(string token)
        {
            var userInfo = await GoogleJsonWebSignature.ValidateAsync(token);
            ValidateToken(userInfo);

            if (!await _db.Users.Exists(userInfo.Email))
            {
                await _db.Users.Add(new UserClaims(userInfo.Name, userInfo.Email));
                await _db.SaveChanges();
            }

            var user = await _db.Users.Find(userInfo.Email);

            return GenerateJwtToken(GenerateClaims(user));
        }

        private IEnumerable<Claim> GenerateClaims(UserClaims user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };
        }

        private static void ValidateToken(GoogleJsonWebSignature.Payload userInfo)
        {
            if (userInfo == null)
            {
                throw new ArgumentException(ErrorMessages.INVALID_TOKEN);
            }
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var token = _config.SecurityToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
