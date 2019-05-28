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
		public async Task<int> Availability(int id)
		{
			return (await Find(id)).Quantity;
		}

		public Task<bool> Exists(int id)
        {
            //TODO faster way
            return Task.Run(() => _set.Any(r => r.Id == id));
        }

        public Task<Record> Find(int id)
        {
            return Task.Run(() => _set.FirstOrDefault(r => r.Id == id));
        }
    }
}
