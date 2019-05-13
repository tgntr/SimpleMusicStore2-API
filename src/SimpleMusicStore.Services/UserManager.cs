using SimpleMusicStore.Contracts;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;

namespace SimpleMusicStore.Services
{
    public class UserManager : IdentityHandler
    {
        public bool Exists(AuthenticationRequest request)
        {
			//TODO add repository
            return true;
        }
    }
}
