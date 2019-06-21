using AutoMapper;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts;
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
        private readonly IRecordRepository _records;
        private readonly IMapper _mapper;
        private readonly ILabelService _labels;
        private readonly IArtistService _artists;
        private readonly IServiceValidator _validator;
        private readonly Sorter _sorter;
        private readonly ICurrentUserActivities _currentUser;
        private readonly FileStorage _googleCloud;

        public RecordService(IRecordRepository records,
            IMapper mapper,
            ILabelService labels,
            IArtistService artists,
            IServiceValidator validator,
            Sorter sorter,
            ICurrentUserActivities currentUser,
            FileStorage googleCloud)
        {
            _records = records;
            _mapper = mapper;
            _artists = artists;
            _validator = validator;
            _sorter = sorter;
            _currentUser = currentUser;
            _googleCloud = googleCloud;
            _labels = labels;
        }
        public async Task Add(RecordInfo record)
        {
            await _validator.RecordIsNotInStore(record.Id);
            CreateArtistAndLabelProfiles(record);
            await AddRecordToStore(record);

            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                await UploadTrackPreviews(record);
            }).Start();
        }

        public async Task<RecordView> Find(int id)
        {
            return await GenerateRecordView(id);
        }

        public NewsFeed NewsFeed()
        {
            return new NewsFeed
            {
                Recommended = _sorter.Sort(SortTypes.Recommendation, _records.FindAll()),
                MostPopular = _sorter.Sort(SortTypes.Popularity, _records.FindAll()),
                Newest = _sorter.Sort(SortTypes.DateAdded, _records.FindAll())
            };
        }

        public async Task AddStock(int recordId, int quantity)
        {
            await _records.AddStock(recordId, quantity);
            await _records.SaveChanges();
        }

        private async Task AddRecordToStore(RecordInfo recordInfo)
        {
            var record = _mapper.Map<Record>(recordInfo);
            record.Stocks.Add(new Stock(recordInfo.Quantity));
            await _records.Add(record);
            await _records.SaveChanges();
        }

        private void CreateArtistAndLabelProfiles(RecordInfo recordInfo)
        {
            Task.WaitAll(
                _artists.Add(recordInfo.ArtistId()),
                _labels.Add(recordInfo.LabelId()));
        }

        private async Task<RecordView> GenerateRecordView(int id)
        {
            var record = await _records.Find(id);
            record.IsInWishlist = _currentUser.IsRecordInWishlist(id);
            return record;
        }

        private async Task UploadTrackPreviews(RecordInfo record)
        {
            foreach (var track in record.Tracklist)
            {
                await _googleCloud.Upload(track.Preview, record.Id + track.Title);
            }
        }
    }
}
