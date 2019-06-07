using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
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
			var wish = await _set.FirstAsync(w => w.RecordId == recordId && w.UserId == userId);
			_set.Remove(wish);
			await SaveChanges();
		}
    }
}
