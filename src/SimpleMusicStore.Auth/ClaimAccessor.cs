using Microsoft.AspNetCore.Http;
using SimpleMusicStore.Constants;
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

        public string Id => FindClaim(AuthConstants.ID);
        public string Username => FindClaim(AuthConstants.USERNAME);
        public bool IsAdmin => bool.Parse(FindClaim(AuthConstants.IS_ADMIN));
        public bool IsAuthenticated => _claims.Identity.IsAuthenticated;
        private string FindClaim(string type) => _claims.FindFirstValue(type);
    }
}
