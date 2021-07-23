﻿using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _db;
        private readonly IServiceValidator _validator;
        private readonly ICurrentUserActivities _currentUser;
        private readonly Sorter _sorter;

        public RecordService(IUnitOfWork db,
            Sorter sorter,
            IServiceValidator validator,
            ICurrentUserActivities currentUser)
        {
            _db = db;
            _sorter = sorter;
            _validator = validator;
            _currentUser = currentUser;
        }
        public async Task Add(NewRecord record)
        {
            await _validator.RecordIsNotInStore(record.Id);
            CreateArtistAndLabelProfiles(record);
            await AddRecordToStore(record);
        }

        public async Task<RecordView> Find(int id)
        {
            var record = await _db.Records.Find(id);
            record.IsInWishlist = _currentUser.IsRecordInWishlist(id);
            return record;
        }

        public NewsFeed NewsFeed()
        {
            var records = _db.Records.FindAllInStock();

            return new NewsFeed
            {
                MostPopular = _sorter.Sort(SortTypes.Popularity, records).Take(6),
                Newest = _sorter.Sort(SortTypes.DateAdded, records).Take(6),
                Recommended = _sorter.Sort(SortTypes.Recommendation, records).Take(6)
            };
        }

        public async Task AddStock(int recordId, int quantity)
        {
            await _validator.RecordExists(recordId);
            await _db.Stocks.Add(recordId, quantity);
            await _db.SaveChanges();
        }

        private async Task AddRecordToStore(NewRecord record)
        {
            await _db.Records.Add(record);
            await _db.Stocks.Add(record.Id, record.Quantity);
            await _db.SaveChanges();
        }

        private void CreateArtistAndLabelProfiles(NewRecord recordInfo)
        {
            Task.WaitAll(
                _db.Artists.Add(recordInfo.Artist),
                _db.Labels.Add(recordInfo.Label));
        }
    }
}
