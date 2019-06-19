using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMusicStore.Sorting
{
    public class PriceAscendingSort : SortType
    {
        public IEnumerable<RecordDetails> Sort(IEnumerable<RecordDetails> records)
        {
            return records.OrderBy(r => r.Price);
        }
    }
}
