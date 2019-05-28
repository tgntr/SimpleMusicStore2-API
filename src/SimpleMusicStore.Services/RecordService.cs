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
        private readonly IServiceValidations _validator;

        public RecordService(
			MusicSource source,
			IRecordRepository records,
			IMapper mapper,
			ILabelService labels,
			IArtistService artists,
            IServiceValidations validator)
		{
			_discogs = source;
			_records = records;
			_mapper = mapper;
			_artists = artists;
            _validator = validator;
            _labels = labels;
		}

		public async Task Add(NewRecord record)
		{
			var recordInfo = await ExtractRecordInfo(record.DiscogsUrl);
			await _validator.RecordIsNotInStore(recordInfo.Id);
			CreateArtistAndLabelProfiles(recordInfo);
			await AddRecordToStore(recordInfo, record.Price);
		}

		private async Task AddRecordToStore(RecordInfo recordInfo, decimal price)
		{
			var record = _mapper.Map<Record>(recordInfo);
			record.Price = record.Price;
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
