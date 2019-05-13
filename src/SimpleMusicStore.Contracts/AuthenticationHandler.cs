using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts
{
    public interface AuthenticationHandler
    {
        Task<string> Authenticate(AuthenticationRequest request);
    }
}
