using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Sorting;
using SimpleMusicStore.Models.Binding;
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

        public IEnumerable<RecordDetails> FindAll()
        {
            return _set.Select(_mapper.Map<RecordDetails>);
        }

        public async Task<int> Availability(int id)
		{
			return (await _set.FindAsync(id)).Availability();
		}

		public Task<bool> Exists(int id)
        {
            return _set.AnyAsync(r => r.Id == id);
        }

        public async Task<RecordView> Find(int id)
        {
            return _mapper.Map<RecordView>(await _set.FindAsync(id));
        }

        public IEnumerable<RecordDetails> FindAll(FilterCriterias criterias)
        {
            return ((IEnumerable<Record>)_set)
                .FilterByGenre(criterias.Genres)
                .FilterByFormat(criterias.Formats)
                .Select(_mapper.Map<RecordDetails>);
        }

        public IEnumerable<RecordDetails> FindAll(string searchTerm)
        {
            return ((IEnumerable<Record>)_set)
                .Search(searchTerm)
                .Select(_mapper.Map<RecordDetails>);
        }

        public IEnumerable<string> AvailableFormats()
        {
            //TODO CACHE
            return _set.Select(r => r.Format).Distinct();
        }

        public IEnumerable<string> AvailableGenres()
        {
            //TODO CACHE
            return _set.Select(r => r.Genre).Distinct();
        }

        public async Task AddStock(int recordId, int quantity)
        {
            var record = await _set.FindAsync(recordId);
            record.Stocks.Add(new Stock(quantity));
        }
    }
}
