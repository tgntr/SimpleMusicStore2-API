using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
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
			return (await _set.FirstOrDefaultAsync(r => r.Id == id)).Quantity;
		}

		public Task<bool> Exists(int id)
        {
            //TODO faster way
            return _set.AnyAsync(r => r.Id == id);
        }

        public async Task<RecordView> Find(int id)
        {
            return _mapper.Map<RecordView>(await _set.FirstAsync(r => r.Id == id));
        }

        public IEnumerable<RecordDetails> FindAll(FilterCriterias criterias)
        {
            IEnumerable<Record> filteredRecords = _set;
            filteredRecords = FilterByFormat(criterias.Formats, filteredRecords);
            filteredRecords = FilterByGenre(criterias.Genres, filteredRecords);

            return filteredRecords.Select(_mapper.Map<RecordDetails>);
        }

        public IEnumerable<RecordDetails> FindAll(string[] keywords)
        {
            return FilterByKeywords(keywords).Select(_mapper.Map<RecordDetails>);
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

        private IEnumerable<Record> FilterByGenre(IEnumerable<string> genres, IEnumerable<Record> filteredRecords)
        {
            if (genres.Any())
                filteredRecords = filteredRecords.Where(r => genres.Contains(r.Genre));
            return filteredRecords;
        }

        private IEnumerable<Record> FilterByFormat(IEnumerable<string> formats, IEnumerable<Record> filteredRecords)
        {
            if (formats.Any())
                filteredRecords = filteredRecords.Where(r => formats.Contains(r.Format));
            return filteredRecords;
        }

        private IEnumerable<Record> FilterByKeywords(string[] keywords)
        {
            //TODO check if it searches properly and find a way to order them by relevance
            return _set.Where(r =>
                keywords.Any(kw => r.Title.ToLower().Contains(kw)) ||
                keywords.Any(kw => r.Artist.Name.ToLower().Contains(kw)) ||
                keywords.Any(kw => r.Label.Name.ToLower().Contains(kw)));
        }
    }
}
