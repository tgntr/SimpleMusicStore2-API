using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;

namespace SimpleMusicStore.Contracts
{
    public interface AuthenticationHandler
    {
        bool TryAuthenticate(AuthenticationRequest request, out string token);
    }
}
