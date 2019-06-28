using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;

namespace SimpleMusicStore.Api.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artists;

        public ArtistController(IArtistService artists)
            : base()
        {
            _artists = artists;
        }
        public Task<ArtistView> Details(int id)
        {
            return _artists.Find(id);
        }
    }
}