using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Extensions;
using SimpleMusicStore.Models.Binding;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SimpleMusicStore.Api.Controllers
{
    [Authorize]
    public class DiscogsController : Controller
    {
        private readonly MusicSource _discogs;

        public DiscogsController(MusicSource discogs)
            : base()
        {
            _discogs = discogs;
        }

        public Task<NewRecord> Find([RegularExpression(DiscogsConstants.DISCOGS_URL_PATTERN)] string url)
        {
            return _discogs.ExtractInformation(url.AsUri());
        }
    }
}