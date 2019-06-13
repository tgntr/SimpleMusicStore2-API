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
        private readonly MusicSource _discogs;
        private readonly IRecordRepository _records;
        private readonly IMapper _mapper;
        private readonly ILabelService _labels;
        private readonly IArtistService _artists;
        private readonly IServiceValidations _validator;
        private readonly Sorter _sorter;

        public RecordService(MusicSource source,
            IRecordRepository records,
            IMapper mapper,
            ILabelService labels,
            IArtistService artists,
            IServiceValidations validator,
            Sorter sorter)
        {
            _discogs = source;
            _records = records;
            _mapper = mapper;
            _artists = artists;
            _validator = validator;
            _sorter = sorter;
            _labels = labels;
        }

        public async Task Add(NewRecord record)
        {
            var recordInfo = await ExtractRecordInfo(record.DiscogsUrl);
            await _validator.RecordIsNotInStore(recordInfo.Id);
            CreateArtistAndLabelProfiles(recordInfo);
            await AddRecordToStore(recordInfo, record.Price);
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

        private async Task AddRecordToStore(RecordInfo recordInfo, decimal price)
        {
            var record = _mapper.Map<Record>(recordInfo);
            record.Price = price;
            await _records.Add(record);
            await _records.SaveChanges();
        }

        private void CreateArtistAndLabelProfiles(RecordInfo recordInfo)
        {
            Task.WaitAll(
                _artists.Add(recordInfo.ArtistId),
                _labels.Add(recordInfo.LabelId));
        }

        private async Task<RecordInfo> ExtractRecordInfo(string url)
        {
            return await _discogs.Record(new Uri(url));
        }
    }
}
