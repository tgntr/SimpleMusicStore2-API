using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ILabelFollowRepository
    {
        Task Add(int labelId, string userId);
        Task<bool> Exists(int labelId, string userId);
		Task Delete(int labelId, string userId);
    }
}
