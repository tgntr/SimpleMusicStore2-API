using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;

namespace SimpleMusicStore.Api.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecordService _records;
        private readonly UserManager<SimpleUser> _users;

        public RecordController(IRecordService records, UserManager<SimpleUser> users)
            : base()
        {
            _records = records;
            _users = users;
        }

        [HttpPost]
        public async Task Add([FromBody]NewRecord record)
        {
            await _users.CreateAsync(new SimpleUser { UserName = "tgntr" }, "test");
            //TODO in the front end, when someone paste discogs url, provide an preview with some AJAX, so the user could see what he is going to add to the store????
            await _records.Add(record);
        }


    }
}