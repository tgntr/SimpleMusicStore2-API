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
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _db;
        private readonly FileStorage _storage;
        private readonly IBackgroundTaskQueue _backgroundThread;
        private readonly Sorter _sorter;

        public RecordService(IUnitOfWork db,
            Sorter sorter,
            FileStorage storage,
            IBackgroundTaskQueue backgroundThread)
        {
            _db = db;
            _sorter = sorter;
            _storage = storage;
            _backgroundThread = backgroundThread;
        }
        public async Task Add(NewRecord record)
        {
            await _db.Validator.RecordIsNotInStore(record.Id);
            CreateArtistAndLabelProfiles(record);
            await AddRecordToStore(record);
            //TODO test 
            UploadTrackPreviewsInBackgroundThread(record);

            
        }

        public async Task<RecordView> Find(int id)
        {
            return await GenerateRecordView(id);
        }

        public NewsFeed NewsFeed()
        {
            return new NewsFeed
            {
                Recommended = _sorter.Sort(SortTypes.Recommendation, _db.Records.FindAll()),
                MostPopular = _sorter.Sort(SortTypes.Popularity, _db.Records.FindAll()),
                Newest = _sorter.Sort(SortTypes.DateAdded, _db.Records.FindAll())
            };
        }

        public async Task AddStock(int recordId, int quantity)
        {
            await _db.Validator.RecordExists(recordId);
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
            record.IsInWishlist = _db.CurrentUser.IsRecordInWishlist(id);
            return record;
        }

        private void UploadTrackPreviewsInBackgroundThread(NewRecord record)
        {
            foreach (var track in record.Tracklist)
            {
                _backgroundThread.QueueBackgroundWorkItem(async token =>
                {
                    await _storage.Upload(track.Preview, record.Id + track.Title);
                });
            }
        }
    }
}
