using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class LabelFollowRepository : DbRepository<LabelFollow>, ILabelFollowRepository
    {
        public LabelFollowRepository(SimpleMusicStoreDbContext db)
            : base(db)
        {
        }

		public async Task Delete(int labelId, string userId)
        {
            var labelFollow = await Find(labelId, userId);
            _set.Remove(labelFollow);
            await SaveChanges();
        }

        private async Task<LabelFollow> Find(int labelId, string userId)
        {
            return await _set.FirstAsync(lf => lf.LabelId == labelId && lf.UserId == userId);
        }

        public Task<bool> Exists(int labelId, string userId)
        {
            return _set.AnyAsync(f => f.LabelId == labelId && f.UserId == userId);
        }
    }
}
