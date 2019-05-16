using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class ArtistRepository : ListRepository<Artist>, IArtistRepository
    {
        public Task<bool> Exists(int id)
        {
            return Task.Run(() => _set.Any(a => a.Id == id));
        }
    }
}
