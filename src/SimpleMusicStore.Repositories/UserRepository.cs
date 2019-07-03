using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class UserRepository : DbRepository<User>, IUserRepository
    {
        public UserRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            :base(db, mapper)
        {
        }
        
        public async Task<UserDetails> Find(string id)
        {
            var user = await _set.FindAsync(id);
            ValidateThatUserExists(user);
            return _mapper.Map<UserDetails>(user);
        }

        private void ValidateThatUserExists(User user)
        {
            if (user == null)
                throw new ArgumentException(ErrorMessages.INVALID_USER);
        }

        public Task Add(ClaimsPrincipal user)
        {
            return _set.AddAsync(_mapper.Map<User>(user));
        }

        public Task<bool> Exists(string id)
        {
            return _set.AnyAsync(u => u.Id == id);
        }

        public IEnumerable<SubscriberDetails> Subscribers()
        {
            return _set.Where(u => u.IsSubscribed).Select(_mapper.Map<SubscriberDetails>);
        }
    }
}
