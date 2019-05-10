using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Authentication;
using System;

namespace SimpleMusicStore.Contracts
{
    public interface AuthenticationProvider
    {
        bool Authenticate(AuthenticationRequest request, out string token);
    }
}
