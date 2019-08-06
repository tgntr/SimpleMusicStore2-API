using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Auth
{
    public interface IClaimAccessor
    {
        string Id { get; }
        //string Email { get; }
        //string FullName { get; }
        bool IsAuthenticated { get; }
    }
}
