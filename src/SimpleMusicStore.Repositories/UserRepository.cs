//using SimpleMusicStore.Contracts.Repositories;
//using SimpleMusicStore.Entities;
//using SimpleMusicStore.Models.AuthenticationProviders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SimpleMusicStore.Repositories
//{
//    public class UserRepository : ListRepository<User>, IUserRepository
//    {
//        public UserRepository()
//        {
//            _set.Add(new User { Id = "asd", UserName = "test", PasswordHash = "test" });
//        }
//        public Task<bool> IsValid(AuthenticationRequest request)
//        {
//            return Task.Run(() => _set.Any(u => u.UserName == request.Username && u.Password == request.Password));
//        }

//        public Task<User> Find(string username)
//        {
//            return Task.Run(() => _set.FirstOrDefault(u => u.Username == u.Username));
//        }
//    }
//}
