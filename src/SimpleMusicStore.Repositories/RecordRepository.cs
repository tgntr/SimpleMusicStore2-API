using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class RecordRepository : DbRepository<Record>, IRecordRepository
    {
        public RecordRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            : base(db, mapper)
        {

        }

        public IEnumerable<RecordDetails> All()
        {
            return _set.Select(_mapper.Map<RecordDetails>);
        }

        public async Task<int> Availability(int id)
		{
			return (await Find(id)).Quantity;
		}

		public Task<bool> Exists(int id)
        {
            //TODO faster way
            return _set.AnyAsync(r => r.Id == id);
        }

        public Task<Record> Find(int id)
        {
            return _set.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
