using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Sorting
{
    public interface ISortingStrategyFactory
    {
        SortingStrategy Create(string sort);
    }
}
