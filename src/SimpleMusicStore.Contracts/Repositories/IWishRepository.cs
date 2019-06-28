using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IWishRepository
    {
        Task Add(int recordId, string userId);
        Task<bool> Exists(int recordId, string userId);
		Task Delete(int recordId, string userId);
	}
}
