using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IWishRepository : IRepository<Wish>
    {
        Task<bool> Exists(int recordId, string userId);
    }
}
