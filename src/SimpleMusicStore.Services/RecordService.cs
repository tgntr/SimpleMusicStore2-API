using AutoMapper;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
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

            if (!await _records.Exists(recordInfo.Id))
                throw new ArgumentException("record is already in store");

            await _labels.Add(recordInfo.LabelId);
            await _artists.Add(recordInfo.LabelId);
            //TODO configure map
            var record = _mapper.Map<Record>(recordInfo);
            await _records.Add(record);
        }


    }
}
