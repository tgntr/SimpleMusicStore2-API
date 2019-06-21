using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using SimpleMusicStore.Sorting;
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
            var artist = await _set.FindAsync(id);
            ValidateThatArtistExists(artist);
            return _mapper.Map<ArtistView>(artist);
        }

        public IEnumerable<ArtistDetails> FindAll(string searchTerm)
        {
            return ((IEnumerable<Artist>)_set)
                .Search(searchTerm)
                .Select(_mapper.Map<ArtistDetails>);
        }

        private static void ValidateThatArtistExists(Artist artist)
        {
            if (artist == null)
                throw new ArgumentException(ErrorMessages.INVALID_ARTIST);
        }

    }
}
