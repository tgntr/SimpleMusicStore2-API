using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IRecordRepository : IRepository<Record>
    {
        Task<bool> Exists(int id);
        Task<Record> Find(int id);
		Task<int> Availability(int id);
        IEnumerable<RecordDetails> FindAll();
        IEnumerable<RecordDetails> FindAll(FilterCriterias criterias);
        IEnumerable<string> AvailableFormats();
        IEnumerable<string> AvailableGenres();
    }
}
