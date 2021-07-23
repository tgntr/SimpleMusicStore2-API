using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _db;

        public UserService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task Add(ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!await _db.Users.Exists(userId))
            {
                //await _db.Users.Add(user);
                //await _db.SaveChanges();
            }
        }
    }
}
