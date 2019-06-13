using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsValid(AuthenticationRequest request);

        Task<User> Find(string username);
    }
}
