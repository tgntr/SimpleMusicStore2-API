using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;

namespace SimpleMusicStore.Services
{
    public class BrowseService : IBrowseService
    {
        private readonly IRecordRepository _records;

        public BrowseService(IRecordRepository records)
        {
            _records = records;
        }

        public Browse GenerateBrowseView()
        {
            return new Browse
            {
                AvailableFormats = _records.AvailableFormats(),
                AvailableGenres = _records.AvailableGenres(),
                Records = _records.FindAll()
            };
        }

        public IEnumerable<RecordDetails> Filter(FilterCriterias criterias)
        {
            return _records.FindAll(criterias);
        }
    }
}
