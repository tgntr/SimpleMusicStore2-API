using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class UserRepository : ListRepository<User>, IUserRepository
    {
        public UserRepository()
        {
            set.Add(new User { Username = "test", Password = "test" });
        }
        public Task<bool> IsValid(AuthenticationRequest request)
        {
            return Task.Run(() => set.Any(u => u.Username == request.Username && u.Password == request.Password));
        }

        public Task<User> Find(string username)
        {
            return Task.Run(() => set.FirstOrDefault(u => u.Username == u.Username));
        }
    }
}
