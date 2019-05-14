using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsValid(AuthenticationRequest request);

        Task<User> Find(string username);
    }
}
