using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IUserRepository : IRepository<SimpleUser>
    {
        Task<bool> IsValid(AuthenticationRequest request);

        Task<SimpleUser> Find(string username);
    }
}
