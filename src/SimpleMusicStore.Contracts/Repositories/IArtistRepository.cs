using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<bool> Exists(int id);
        Task<ArtistView> Find(int id);
        IEnumerable<ArtistDetails> FindAll(string searchTerm);
    }
}
