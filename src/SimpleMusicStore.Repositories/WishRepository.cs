using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using System;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class WishRepository : DbRepository<Wish>, IWishRepository
    {
        public WishRepository(SimpleMusicStoreDbContext db)
            :base(db)
        {
        }
        public Task<bool> Exists(int recordId, string userId)
        {
            return _set.AnyAsync(w => w.RecordId == recordId && w.UserId == userId);
        }

		public async Task Delete(int recordId, string userId)
        {
            var wish = await _set.FindAsync(recordId, userId);
            ValidateThatWishExists(wish);
            _set.Remove(wish);
        }

        private static void ValidateThatWishExists(Wish wish)
        {
            if (wish == null)
                throw new ArgumentException(ErrorMessages.RECORD_NOT_IN_WISHLIST);
        }
    }
}
