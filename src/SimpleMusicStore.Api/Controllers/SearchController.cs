using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;

namespace SimpleMusicStore.Api.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBrowseService _browser;

        public SearchController(IBrowseService browser)
        {
            _browser = browser;
        }
        
        [Route("search")]
        public IActionResult Index()
        {
            return View();
        }
    }
}