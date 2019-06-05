using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Auth
{
    public interface AuthenticationHandler
    {
        Task<string> Authenticate(AuthenticationRequest request);
    }
}
