using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts;

namespace SimpleMusicStore.Api.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        private readonly FileStorage _googleCloud;

        public TrackController(FileStorage googleCloud)
        {
            _googleCloud = googleCloud;
        }

        [HttpPost]
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            return await _googleCloud.Upload(file, fileName);
        }
    }
}