using AutoMapper;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.MusicLibraries;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class RecordService : IRecordService
    {
        //private readonly MusicSource _discogs;
        private readonly IRecordRepository _records;
        private readonly IMapper _mapper;
        private readonly ILabelService _labels;
        private readonly IArtistService _artists;
        private readonly IServiceValidations _validator;
        private readonly Sorter _sorter;
        private readonly ICurrentUserActivities _currentUser;

        public RecordService(//MusicSource source,
            IRecordRepository records,
            IMapper mapper,
            ILabelService labels,
            IArtistService artists,
            IServiceValidations validator,
            Sorter sorter,
            ICurrentUserActivities currentUser)
        {
            //_discogs = source;
            _records = records;
            _mapper = mapper;
            _artists = artists;
            _validator = validator;
            _sorter = sorter;
            _currentUser = currentUser;
            _labels = labels;
        }
        //TODO remove after test new way
        //public async Task Add(NewRecord record)
        //{
        //    var recordInfo = await ExtractRecordInfo(record.DiscogsUrl);
        //    await _validator.RecordIsNotInStore(recordInfo.Id);
        //    CreateArtistAndLabelProfiles(recordInfo);
        //    await AddRecordToStore(recordInfo);
        //}
        public async Task Add(RecordInfo record)
        {
            await _validator.RecordIsNotInStore(record.Id);
            CreateArtistAndLabelProfiles(record);
            await AddRecordToStore(record);
        }

        public async Task<RecordView> Find(int id)
        {
            await _validator.RecordExists(id);
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

        private async Task AddRecordToStore(RecordInfo recordInfo)
        {
            var record = _mapper.Map<Record>(recordInfo);
            record.Stocks.Add(new Stock { Quantity = recordInfo.Quantity });
            await _records.Add(record);
            await _records.SaveChanges();
        }

        private void CreateArtistAndLabelProfiles(RecordInfo recordInfo)
        {
            Task.WaitAll(
                _artists.Add(recordInfo.ArtistId()),
                _labels.Add(recordInfo.LabelId()));
        }

        //TODO REMOVE AFTER TESTING THE NEW APPROACH
        //private async Task<RecordInfo> ExtractRecordInfo(string url)
        //{
        //    return await _discogs.Record(new Uri(url));
        //}

        private async Task<RecordView> GenerateRecordView(int id)
        {
            var record = await _records.Find(id);
            record.IsInWishlist = _currentUser.IsRecordInWishlist(id);
            return record;
        }
    }
}
