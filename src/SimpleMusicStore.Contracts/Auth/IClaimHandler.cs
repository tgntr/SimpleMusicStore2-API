using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Auth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Auth
{
    public interface IClaimHandler
    {
        Claim[] Generate(UserDetails user);
    }
}
