using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Auth
{
    public interface IClaimHandler
    {
        Claim[] GenerateClaims(SimpleUser user, bool isAdmin);
    }
}
