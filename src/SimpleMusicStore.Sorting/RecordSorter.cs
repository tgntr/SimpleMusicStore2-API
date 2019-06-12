using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;

namespace SimpleMusicStore.Sorting
{
    public class RecordSorter : Sorter
    {
        private readonly ICurrentUserActivities _currentUser;

        public RecordSorter(ICurrentUserActivities currentUser)
        {
            _currentUser = currentUser;
        }

        public SortType Create(SortTypes sort)
        {
            if (sort == SortTypes.Recommendation && _currentUser.IsAuthenticated)
                return new RecommendationSort(_currentUser);
            else if (sort == SortTypes.DateAdded)
                return new DateAddedSort();
            else
                return new PopularitySort();
        }

        public IEnumerable<RecordDetails> Sort(SortTypes sort, IEnumerable<RecordDetails> records)
        {
            if (sort == SortTypes.Recommendation && _currentUser.IsAuthenticated)
                return new RecommendationSort(_currentUser).Sort(records);
            else if (sort == SortTypes.DateAdded)
                return new DateAddedSort().Sort(records);
            else
                return new PopularitySort().Sort(records);
        }
    }
}
