using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Models.Auth;
using System.Security.Claims;

namespace SimpleMusicStore.Auth
{
    public class ClaimHandler : IClaimHandler
    {
        public Claim[] Generate(UserDetails user)
        {
            return new Claim[]
            {
                new Claim(AuthConstants.USERNAME, user.UserName),
                new Claim(AuthConstants.ID, user.Id),
                new Claim(AuthConstants.IS_ADMIN, user.IsAdmin.ToString())
            };
        }
    }
}
