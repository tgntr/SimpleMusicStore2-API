using Microsoft.Extensions.Options;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Models.AuthenticationProviders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SimpleMusicStore.Auth.Extensions;
using System.Threading.Tasks;
using SimpleMusicStore.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Entities;

namespace SimpleMusicStore.Auth
{
    public class Jwt : AuthenticationHandler
    {
        private readonly IServiceValidations _validator;
        private readonly UserManager<User> _users;
        private readonly JwtConfiguration _config;
        private readonly IClaimHandler _claimCreator;

        public Jwt(IOptions<JwtConfiguration> config, IServiceValidations validator, UserManager<User> users, IClaimHandler claimCreator)
        {
            _validator = validator;
            _users = users;
            _config = config.Value;
            _claimCreator = claimCreator;
        }

        public async Task<string> Authenticate(AuthenticationRequest request)
        {
            var user = await _users.FindByNameAsync(request.Username);
            await _validator.CredentialsAreValid(user, request.Password);
            var claims = await ExtractClaims(user);
            return GenerateJwtToken(claims);
        }

        private async Task<Claim[]> ExtractClaims(User user)
        {
            return _claimCreator.GenerateClaims(user, await _users.IsInRoleAsync(user, "Admin"));
        }

        private string GenerateJwtToken(Claim[] claims)
        {
            var token = _config.SecurityToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
