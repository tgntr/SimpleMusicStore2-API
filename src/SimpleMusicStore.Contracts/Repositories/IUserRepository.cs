using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Exists(string id);
        Task Add(ClaimsPrincipal user);
        Task<UserDetails> Find(string id);
    }
}
