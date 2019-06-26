using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task Add([FromBody] NewRecord record)
        {
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

        public async Task<RecordView> Details(int id)
        {
            return await _records.Find(id);
        }

        [HttpPost]
        public async Task AddStock(int recordId , [FromBody,Range(1, 100)] int quantity)
        {
            await _records.AddStock(recordId, quantity);
        }
    }
}