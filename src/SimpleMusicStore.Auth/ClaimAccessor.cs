using Microsoft.AspNetCore.Http;
using SimpleMusicStore.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SimpleMusicStore.Auth
{
    public class ClaimAccessor : IClaimAccessor
    {
        private readonly ClaimsPrincipal _claims;

        public ClaimAccessor(IHttpContextAccessor http)
        {
            _claims = http.HttpContext.User;
        }

        public string Id => FindClaim(AuthClaimTypes.ID);
        public string Username => FindClaim(AuthClaimTypes.USERNAME);
        public bool IsAdmin => bool.Parse(FindClaim(AuthClaimTypes.IS_ADMIN));
        public bool IsAuthenticated => _claims.Identity.IsAuthenticated;
        private string FindClaim(string type) => _claims.FindFirstValue(type);
    }
}
