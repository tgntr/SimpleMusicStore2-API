using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class RecordRepository : ListRepository<Record>, IRecordRepository
    {
        public Task<bool> Exists(int id)
        {
            //TODO faster way
            return Task.Run(() => _set.Any(r => r.Id == id));
        }
    }
}
