using SimpleMusicStore.Entities;
using SimpleMusicStore.Models;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Exists(string email);
        Task Add(UserClaims user);
        Task<UserDetails> Find(int id);
        Task<UserClaims> Find(string email);
        IEnumerable<SubscriberDetails> Subscribers();
    }
}
