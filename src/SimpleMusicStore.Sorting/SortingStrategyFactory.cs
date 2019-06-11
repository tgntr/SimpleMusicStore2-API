using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using System;

namespace SimpleMusicStore.Sorting
{
    public class SortingStrategyFactory : ISortingStrategyFactory
    {
        private readonly ICurrentUser _currentUser;

        public SortingStrategyFactory(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public SortingStrategy Create(SortTypes sort)
        {
            if (sort == SortTypes.Recommendation && _currentUser.IsAuthenticated)
                return new RecommendationSort(_currentUser);
            else if (sort == SortTypes.DateAdded)
                return new DateAddedSort();
            else
                return new PopularitySort();
        }
    }
}
