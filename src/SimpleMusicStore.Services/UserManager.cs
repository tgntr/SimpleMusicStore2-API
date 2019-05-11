using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;

namespace SimpleMusicStore.Services
{
    public class UserManager : IdentityHandler
    {
        public bool IsValidUser(AuthenticationRequest request)
        {
			//TODO
            return true;
        }
    }
}
