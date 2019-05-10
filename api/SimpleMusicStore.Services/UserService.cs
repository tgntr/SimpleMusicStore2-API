using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Authentication;
using System;

namespace SimpleMusicStore.Services
{
    //TODO naming - proper name without repeating the Service word
    public class UserService : UserManager
    {
        public bool IsValidUser(AuthenticationRequest request)
        {
            return true;
        }
    }
}
