using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;

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
        public async Task Add([FromBody]NewRecord record)
        {
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

        public async Task<RecordView> Details(int id)
        {
            return await _records.Find(id);
        }


    }
}