using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Api.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBrowseService _browser;

        public SearchController(IBrowseService browser)
        {
            _browser = browser;
        }

        public IEnumerable<SearchResult> Index([Required, MinLength(1)] string searchTerm)
        {
            return _browser.Search(searchTerm);
        }
    }
}