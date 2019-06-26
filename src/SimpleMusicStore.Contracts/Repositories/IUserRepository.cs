using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Auth;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<UserDetails> Find(AuthenticationRequest credentials);
    }
}
