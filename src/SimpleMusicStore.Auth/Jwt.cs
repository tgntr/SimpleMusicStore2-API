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

namespace SimpleMusicStore.Auth
{
    public class Jwt : AuthenticationHandler
    {
        private readonly JwtConfiguration _config;
        private readonly IUnitOfWork _db;
        private readonly IClaimHandler _claims;

        public Jwt(IOptions<JwtConfiguration> config, IUnitOfWork db, IClaimHandler claimCreator)
        {
            _config = config.Value;
            _db = db;
            _claims = claimCreator;
        }

        public async Task<string> Authenticate(AuthenticationRequest credentials)
        {
            var user = await _db.Users.Find(credentials);
            return GenerateJwtToken(_claims.Generate(user));
        }

        private string GenerateJwtToken(Claim[] claims)
        {
            var token = _config.SecurityToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
