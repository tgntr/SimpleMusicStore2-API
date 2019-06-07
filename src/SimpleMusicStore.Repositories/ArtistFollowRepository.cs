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
    public class ArtistFollowRepository : DbRepository<ArtistFollow>, IArtistFollowRepository
    {
        public ArtistFollowRepository(SimpleMusicStoreDbContext db)
            : base(db)
        {
        }

        public async Task Delete(int artistId, string userId)
        {
            var artistFollow = await _set.FirstAsync(af => af.ArtistId == artistId && af.UserId == userId);
            _set.Remove(artistFollow);
            await SaveChanges();
        }

        public Task<bool> Exists(int artistId, string userId)
        {
            return _set.AnyAsync(f => f.ArtistId == artistId && f.UserId == userId);
        }
    }
}
