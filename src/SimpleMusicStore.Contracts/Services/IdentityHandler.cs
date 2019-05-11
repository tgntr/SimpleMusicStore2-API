using SimpleMusicStore.Models.AuthenticationProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IdentityHandler
    {
        bool IsValidUser(AuthenticationRequest request);
    }
}
