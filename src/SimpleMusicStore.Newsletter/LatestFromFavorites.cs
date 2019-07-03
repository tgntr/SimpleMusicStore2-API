using SimpleMusicStore.Contracts.Newsletter;
using SimpleMusicStore.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Newsletter
{
    public class LatestFromFavorites : NewsletterGenerator
    {
        private readonly IUnitOfWork _db;
        private readonly Notificator _notificator;

        public LatestFromFavorites(IUnitOfWork db, Notificator notificator)
        {
            _db = db;
            _notificator = notificator;
        }
        public async Task Generate()
        {
            foreach (var user in _db.Users.Subscribers())
            {
                //todo generate a beautiful html
                //todo unsubscribe option
                var recordsByFavoriteArtistsAndLabels = _db.Records
                    .LatestByFavorites(user)
                    .Select(r => $"{r.Artist.Name} - {r.Title} [{r.Label.Name}]");

                await _notificator.Send(user.Email, string.Join(",", recordsByFavoriteArtistsAndLabels));
            }
        }
    }
}
