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
    public class ArtistRepository : DbRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(SimpleMusicStoreDbContext db)
            :base(db)
        {
        }
        public Task<bool> Exists(int id)
        {
            return _set.AnyAsync(a => a.Id == id);
        }
    }
}
