using AutoMapper;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.MusicLibraries;
using System;
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
        public RecordService(MusicSource source, IRecordRepository records, IMapper mapper, ILabelService labels, IArtistService artists)
        {
            _discogs = source;
            _records = records;
            _mapper = mapper;
            _artists = artists;
            _labels = labels;
        }

        public async Task Add(NewRecord newRecord)
        {
            var recordInfo = await _discogs.Record(new Uri(newRecord.DiscogsUrl));
            await CheckIfAlreadyExists(recordInfo);
            Task.WaitAll(
                _artists.Add(recordInfo.ArtistId),
                _labels.Add(recordInfo.LabelId));
            var record = _mapper.Map<Record>(recordInfo);
            record.Price = newRecord.Price;
            await _records.Add(record);
        }

        public Task<bool> Exists(int id)
        {
            return _records.Exists(id);
        }

        public Task<Record> Find(int id)
        {
            return _records.Find(id);
        }

        public async Task<int> Availability(int id)
        {
            return (await _records.Find(id)).Quantity;
        }
        private async Task CheckIfAlreadyExists(RecordInfo recordInfo)
        {
            if (await _records.Exists(recordInfo.Id))
                throw new ArgumentException("record is already in store");
        }
    }
}
