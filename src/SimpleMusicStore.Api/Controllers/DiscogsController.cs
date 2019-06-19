using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Extensions;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.MusicLibraries;

namespace SimpleMusicStore.Api.Controllers
{
    [Authorize]
    public class DiscogsController : Controller
    {
        private readonly MusicSource _discogs;

        public DiscogsController(MusicSource discogs)
        {
            _discogs = discogs;
        }
        public async Task<RecordInfo> Find(string url)
        {
            return await _discogs.Record(url.AsUri());
        }
    }
}