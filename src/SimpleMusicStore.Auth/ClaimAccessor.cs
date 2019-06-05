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

        public string Id => _claims.FindFirstValue(AuthClaimTypes.ID);
        public string Username => _claims.FindFirstValue(AuthClaimTypes.USERNAME);
        public bool IsAdmin => bool.Parse(_claims.FindFirstValue(AuthClaimTypes.IS_ADMIN));
    }
}
