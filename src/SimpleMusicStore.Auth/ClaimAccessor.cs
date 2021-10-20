using Microsoft.AspNetCore.Http;
using SimpleMusicStore.Contracts.Auth;
using System.Security.Claims;

namespace SimpleMusicStore.Auth
{
    public class ClaimAccessor : IClaimAccessor
    {
        private readonly ClaimsPrincipal _claims;

        public ClaimAccessor(IHttpContextAccessor http)
        {
            _claims = http.HttpContext.User;
        }

        public int Id => int.Parse(FindClaim(ClaimTypes.NameIdentifier));
        //public string Email => FindClaim(JwtRegisteredClaimNames.Email);
        public bool IsAuthenticated => _claims.Identity.IsAuthenticated;
        private string FindClaim(string type) => _claims.FindFirstValue(type);
    }
}
