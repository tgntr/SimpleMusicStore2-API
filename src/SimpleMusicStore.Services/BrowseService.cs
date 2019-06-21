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
        private readonly IArtistRepository _artists;
        private readonly ILabelRepository _labels;

        public BrowseService(IRecordRepository records, Sorter sorter, IServiceValidator validator, IArtistRepository artists, ILabelRepository labels)
        {
            _records = records;
            _sorter = sorter;
            _artists = artists;
            _labels = labels;
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
            return _sorter.Sort(criterias.Sort, _records.FindAll(criterias));
        }

        public SearchResult Search(string searchTerm)
        {
            return new SearchResult
            {
                Records = _records.FindAll(searchTerm),
                Artists = _artists.FindAll(searchTerm),
                Labels = _labels.FindAll(searchTerm)
            };
        }

        

        private IEnumerable<string> ExtractAllSortTypes()
        {
            return Enum.GetNames(typeof(SortTypes));
        }
    }
}
