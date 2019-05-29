using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Auth
{
    public class ClaimHandler : IClaimHandler
    {
        public Claim[] GenerateClaims(User user, bool isAdmin)
        {
            return new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id),
                new Claim("isAdmin", isAdmin.ToString())
            };
        }
    }
}
