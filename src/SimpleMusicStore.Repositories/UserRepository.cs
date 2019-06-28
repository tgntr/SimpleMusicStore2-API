using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _users;

        public UserRepository(UserManager<User> users)
        {
            _users = users;
        }
        //TODO make User Role when implementing register
        public async Task<UserDetails> Find(AuthenticationRequest credentials)
        {
            var user = await _users.FindByNameAsync(credentials.Username);
            await ValidateThatPasswordIsCorrect(credentials, user);
            return new UserDetails
            {
                Id = user.Id,
                UserName = user.UserName,
                IsAdmin = await _users.IsInRoleAsync(user, AuthConstants.ADMIN_ROLE)
            };
        }

        private async Task ValidateThatPasswordIsCorrect(AuthenticationRequest credentials, User user)
        {
            if (!await _users.CheckPasswordAsync(user, credentials.Password))
                throw new ArgumentException(ErrorMessages.INVALID_CREDENTIALS);
        }
    }
}
