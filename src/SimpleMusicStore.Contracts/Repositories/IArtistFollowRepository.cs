using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IArtistFollowRepository : IRepository<ArtistFollow>
    {
        Task<bool> Exists(int artistId, string userId);
		Task Delete(int artistId, string userId);
	}
}
