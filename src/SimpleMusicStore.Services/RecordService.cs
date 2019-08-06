using AutoMapper;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.BackgroundServiceProvider;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.MusicLibraries;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _db;
        private readonly IBackgroundTaskQueue _backgroundThread;
        private readonly IServiceValidator _validator;
        private readonly ICurrentUserActivities _currentUser;
        private readonly Sorter _sorter;

        public RecordService(IUnitOfWork db,
            Sorter sorter,
            IBackgroundTaskQueue backgroundThread,
            IServiceValidator validator,
            ICurrentUserActivities currentUser)
        {
            _db = db;
            _sorter = sorter;
            _backgroundThread = backgroundThread;
            _validator = validator;
            _currentUser = currentUser;
        }
        public async Task Add(NewRecord record)
        {
            await _validator.RecordIsNotInStore(record.Id);
            CreateArtistAndLabelProfiles(record);
            await AddRecordToStore(record);
        }

        public Task<RecordView> Find(int id)
        {
            return GenerateRecordView(id);
        }

        public NewsFeed NewsFeed()
        {
            var records = _db.Records.FindAllInStock();

            return new NewsFeed
            {
                MostPopular = _sorter.Sort(SortTypes.Popularity, records).Take(5),
                Newest = _sorter.Sort(SortTypes.DateAdded, records).Take(5),
                Recommended = _sorter.Sort(SortTypes.Recommendation, records).Take(5)
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

        private async Task<RecordView> GenerateRecordView(int id)
        {
            var record = await _db.Records.Find(id);
            record.IsInWishlist = _currentUser.IsRecordInWishlist(id);
            return record;
        }
    }
}
