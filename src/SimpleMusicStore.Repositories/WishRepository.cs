using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class WishRepository : ListRepository<Wish>, IWishRepository
    {
        public Task<bool> Exists(int recordId, string userId)
        {
            return Task.Run(()=>_set.Any(w => w.RecordId == recordId && w.UserId == userId));
        }
    }
}
