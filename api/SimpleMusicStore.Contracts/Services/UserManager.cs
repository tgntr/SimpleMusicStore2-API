using SimpleMusicStore.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Services
{
    public interface UserManager
    {
        bool IsValidUser(AuthenticationRequest request);
    }
}
