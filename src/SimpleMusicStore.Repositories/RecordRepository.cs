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
using SimpleMusicStore.Constants;
using SimpleMusicStore.Models;
using System.Linq.Expressions;

namespace SimpleMusicStore.Repositories
{
    public class RecordRepository : DbRepository<Record>, IRecordRepository
    {
        public RecordRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public Task Add(NewRecord record)
        {
            return _set.AddAsync(_mapper.Map<Record>(record));
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
            var record = await _set.FindAsync(id);
            ValidateThatRecordExists(record);
            return _mapper.Map<RecordView>(record);
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

        public IEnumerable<RecordDetails> LatestByFavorites(SubscriberDetails subscriber)
        {
            return _set
                .Where(r=> IsFromLastSevenDays(r) && IsFromFollowedArtistOrLabel(r, subscriber))
                .Select(_mapper.Map<RecordDetails>);
        }

        private static void ValidateThatRecordExists(Record record)
        {
            if (record == null)
                throw new ArgumentException(ErrorMessages.INVALID_RECORD);
        }

        private bool IsFromFollowedArtistOrLabel(Record record, SubscriberDetails subscriber)
        {
            return subscriber.FollowedArtists.Contains(record.ArtistId) || subscriber.FollowedLabels.Contains(record.LabelId);
        }

        private bool IsFromLastSevenDays(Record record)
        {
            return EF.Functions.DateDiffHour(record.DateAdded, DateTime.UtcNow) <= 168;
        }
    }
}
