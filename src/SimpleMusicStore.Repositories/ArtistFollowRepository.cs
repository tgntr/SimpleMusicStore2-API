using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class ArtistFollowRepository : ListRepository<ArtistFollow>, IArtistFollowRepository
    {
		public async Task Delete(int artistId, string userId)
		{
			var artistFollow = _set.First(af => af.ArtistId == artistId && af.UserId == userId);
			_set.Remove(artistFollow);
			await SaveChanges();
		}

		public Task<bool> Exists(int artistId, string userId)
        {
            return Task.Run(() => _set.Any(f => f.ArtistId == artistId && f.UserId == userId));
        }
    }
}
