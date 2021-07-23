using SimpleMusicStore.Models;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IRecordRepository
    {
        Task Add(NewRecord record);
        Task<bool> Exists(int id);
        Task<RecordView> Find(int id);
        Task<int> Availability(int id);
        IEnumerable<RecordDetails> FindAllInStock();
        IEnumerable<RecordDetails> FindAll(FilterCriterias criterias);
        IEnumerable<SearchResult> FindAll(string searchTerm);
        IEnumerable<string> AvailableFormats();
        IEnumerable<string> AvailableGenres();
        IEnumerable<RecordDetails> LatestByFavorites(SubscriberDetails subscriber);

    }
}
