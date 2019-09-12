using PagedList;
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
using System.Linq;

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

        public IEnumerable<RecordDetails> Filter(FilterCriterias criterias, int page)
        {
            return _sorter.Sort(criterias.Sort, _db.Records.FindAll(criterias))
                .ToPagedList(page, CommonConstants.PAGE_SIZE);

        }

        public IEnumerable<SearchResult> Search(string searchTerm)
        {
            return _db.Records.FindAll(searchTerm)
                .Concat(_db.Artists.FindAll(searchTerm))
                .Concat(_db.Labels.FindAll(searchTerm))
                .OrderByDescending(r => Match(r.Name, searchTerm))
                .Take(10);
        }

        private int Match(string name, string searchTerm)
        {
            if (name.StartsWith(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                return searchTerm.Length * 3;
            else if (name.Split().Any(n => n.StartsWith(searchTerm, StringComparison.InvariantCultureIgnoreCase)))
                return searchTerm.Length * 2;
            else if (name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                return 1;
            else
                return 2;
        }

        private IEnumerable<string> ExtractAllSortTypes()
        {
            return Enum.GetNames(typeof(SortTypes));
        }
    }
}
