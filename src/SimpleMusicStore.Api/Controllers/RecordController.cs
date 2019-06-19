using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Api.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecordService _records;
        private readonly IBrowseService _browser;

        public RecordController(IRecordService records, IBrowseService browser)
            : base()
        {
            _records = records;
            _browser = browser;
        }

        [Authorize]
        [HttpPost]
        public async Task Add([FromBody]RecordInfo record)
        {
            if (!ModelState.IsValid)
            {
                var asd = 2;
            }
            //TODO in the front end, when someone paste discogs url, provide an preview with some AJAX, so the user could see what he is going to add to the store????
            await _records.Add(record);
        }

        public Browse Browse()
        {
            return _browser.GenerateBrowseView();
        }

        [HttpPost]
        public IEnumerable<RecordDetails> Filter([FromBody] FilterCriterias criterias)
        {
            return _browser.Filter(criterias);
        }

        public IEnumerable<RecordDetails> Search(string searchTerm)
        {
            return _browser.Search(searchTerm);
        }

        public async Task<RecordView> Details(int id)
        {
            return await _records.Find(id);
        }


    }
}