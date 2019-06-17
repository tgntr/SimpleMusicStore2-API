using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Extensions;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;

namespace SimpleMusicStore.Services
{
    public class BrowseService : IBrowseService
    {
        private readonly IRecordRepository _records;
        private readonly Sorter _sorter;

        public BrowseService(IRecordRepository records, Sorter sorter)
        {
            _records = records;
            _sorter = sorter;
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
            return _sorter.Sort(criterias.Sort.AsSortType(), _records.FindAll(criterias));
        }
    }
}
