using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Extensions;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Services
{
    public class BrowseService : IBrowseService
    {
        private readonly IRecordRepository _records;
        private readonly Sorter _sorter;
        private readonly IServiceValidator _validator;

        public BrowseService(IRecordRepository records, Sorter sorter, IServiceValidator validator)
        {
            _records = records;
            _sorter = sorter;
            _validator = validator;
        }

        public Browse GenerateBrowseView()
        {
            return new Browse
            {
                AvailableFormats = _records.AvailableFormats(),
                AvailableGenres = _records.AvailableGenres(),
                Records = _records.FindAll(),
                AvailableSortTypes = ExtractAllSortTypes()
            };
        }

        public IEnumerable<RecordDetails> Filter(FilterCriterias criterias)
        {
            return _sorter.Sort(criterias.Sort.AsSortType(), _records.FindAll(criterias));
        }

        public IEnumerable<RecordDetails> Search(string searchTerm)
        {
            _validator.SearchTermIsNotEmpty(searchTerm);
            return _records.FindAll(SplitToKeywords(searchTerm));
        }

        private string[] SplitToKeywords(string searchTerm)
        {
            return searchTerm.ToLower().Split();
        }

        private IEnumerable<string> ExtractAllSortTypes()
        {
            return Enum.GetNames(typeof(SortTypes));
        }
    }
}
