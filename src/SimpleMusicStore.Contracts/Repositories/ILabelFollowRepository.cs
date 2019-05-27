using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ILabelFollowRepository : IRepository<LabelFollow>
    {
        Task<bool> Exists(int labelId, string userId);
    }
}
