using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IBrowseService
    {
        Browse GenerateBrowseView();
        IEnumerable<RecordDetails> Filter(FilterCriterias criterias, int page);
        IEnumerable<SearchResult> Search(string searchTerm);
    }
}
