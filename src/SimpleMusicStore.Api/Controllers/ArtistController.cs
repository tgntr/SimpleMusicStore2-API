﻿using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Newsletter;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using System.Threading.Tasks;

namespace SimpleMusicStore.Api.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artists;
        private readonly NewsletterGenerator _newsletter;
        public ArtistController(IArtistService artists, NewsletterGenerator newsletter)
            : base()
        {
            _artists = artists;
            _newsletter = newsletter;
        }
        public Task<ArtistView> Details(int id)
        {
            return _artists.Find(id);
        }

        [Authorize]
        public void Send()
        {
            RecurringJob.AddOrUpdate<NewsletterGenerator>("weekly", n => n.Generate(), Cron.Minutely);

        }
    }
}