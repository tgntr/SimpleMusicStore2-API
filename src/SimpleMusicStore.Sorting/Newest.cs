using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMusicStore.Sorting
{
    public class Newest : SortingStrategy
    {
        public IEnumerable<RecordDetails> Sort(IEnumerable<RecordDetails> records)
        {
            return records.Reverse();
        }
    }
}
