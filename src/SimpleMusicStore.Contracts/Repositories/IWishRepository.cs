using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IWishRepository
    {
        Task Add(int recordId, int userId);
        Task<bool> Exists(int recordId, int userId);
		Task Delete(int recordId, int userId);
	}
}
