using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class LabelFollowRepository : ListRepository<LabelFollow>, ILabelFollowRepository
    {
        public Task<bool> Exists(int labelId, string userId)
        {
            return Task.Run(()=>_set.Any(f => f.LabelId == labelId && f.UserId == userId));
        }
    }
}
