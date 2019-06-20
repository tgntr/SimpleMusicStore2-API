using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class ArtistRepository : DbRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            :base(db, mapper)
        {
        }

        public Task<bool> Exists(int id)
        {
            return _set.AnyAsync(a => a.Id == id);
        }

        public async Task<ArtistView> Find(int id)
        {
            return _mapper.Map<ArtistView>(await _set.FindAsync(id));
        }
    }
}
