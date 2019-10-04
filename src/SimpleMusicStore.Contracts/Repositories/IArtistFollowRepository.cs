using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IArtistFollowRepository
    {
        Task Add(int artistId, int userId);
        Task<bool> Exists(int artistId, int userId);
		Task Delete(int artistId, int userId);
	}
}
