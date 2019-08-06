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
        private readonly IUnitOfWork _db;
        private readonly Sorter _sorter;

        public BrowseService(IUnitOfWork db, Sorter sorter)
        {
            _db = db;
            _sorter = sorter;
        }

        public Browse GenerateBrowseView()
        {
            return new Browse
            {
                AvailableFormats = _db.Records.AvailableFormats(),
                AvailableGenres = _db.Records.AvailableGenres(),
                AvailableSortTypes = ExtractAllSortTypes()
            };
        }

        public IEnumerable<RecordDetails> Filter(FilterCriterias criterias)
        {
            return _sorter.Sort(criterias.Sort, _db.Records.FindAll(criterias));
        }

        public SearchResult Search(string searchTerm)
        {
            return new SearchResult
            {
                Records = _db.Records.FindAll(searchTerm),
                Artists = _db.Artists.FindAll(searchTerm),
                Labels = _db.Labels.FindAll(searchTerm)
            };
        }

        private IEnumerable<string> ExtractAllSortTypes()
        {
            return Enum.GetNames(typeof(SortTypes));
        }
    }
}
