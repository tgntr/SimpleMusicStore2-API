using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Sorting
{
    public class SortingStrategyFactory : ISortingStrategyFactory
    {
        private readonly ICurrentUser _currentUser;

        public SortingStrategyFactory(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public SortingStrategy Create(string sort)
        {
            sort = sort.ToLower();
            if (sort == "recommended" && _currentUser.IsAuthenticated)
                return new Recommended(_currentUser);
            else if (sort == "newest")
                return new Newest();
            else
                return new Popularity();
        }
    }
}
