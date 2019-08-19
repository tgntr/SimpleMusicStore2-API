using SimpleMusicStore.Models.MusicLibraries;
using Newtonsoft.Json;
using SimpleMusicStore.Contracts;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMusicStore.MusicLibrary.Extensions;
using Microsoft.Extensions.Configuration;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Models.Binding;

namespace SimpleMusicStore.MusicLibrary
{
    public class Discogs : MusicSource
    {

        private readonly string _key;
        private readonly string _secret;
        private readonly string _urlFormat = "https://api.discogs.com/{0}/{1}?key={2}&secret={3}";

        public Discogs(IConfigurationSection credentials)
        {
            _key = credentials["Key"];
            _secret = credentials["Secret"];
        }
        public async Task<NewRecord> ExtractInformation(Uri uri)
        {
            var discogsId = await FindId(uri);
            var record = await DownloadContent<NewRecord>(DiscogsConstants.RELEASE, discogsId);
            record.Label = await DownloadContent<LabelInfo>(DiscogsConstants.LABEL, record.LabelId());
            record.Artist = await ExtractArtistInformation(record.ArtistId());

            return record;
        }

        private Task<ArtistInfo> ExtractArtistInformation(string id)
        {
            if (id == DiscogsConstants.VARIOUS_ARTISTS_ID)
            {
                return Task.Run(()=>new ArtistInfo
                {
                    Id = DiscogsConstants.VARIOUS_ARTISTS_ID,
                    Name = DiscogsConstants.VARIOUS_ARTISTS
                });
            }
            else
            {
                return DownloadContent<ArtistInfo>(DiscogsConstants.ARTIST, id);
            }
        }

        private async Task<string> FindId(Uri uri)
        {
            if (IsMasterUrl(uri))
                return (await DownloadContent<MasterInfo>(DiscogsConstants.MASTER, uri.FindDiscogsId())).Main_Release;

            return uri.FindDiscogsId();
        }

        private async Task<T> DownloadContent<T>(string contentType, string discogsId)
        {
            try
            {
                var content = await Web().DownloadStringTaskAsync(GenerateUrl(contentType, discogsId));
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorMessages.INVALID_DISCOGS_URL);
            }
        }

        private WebClient Web()
        {
            var web = new WebClient();
            web.AddHeaders();
            return web;
        }

        private bool IsMasterUrl(Uri uri)
        {
            return uri.AbsolutePath.Split(DiscogsConstants.PARAMETER_SPLITTER).Contains("master");
        }

        private string GenerateUrl(string contentType, string discogsId)
        {
            return string.Format(_urlFormat, contentType, discogsId, _key, _secret);
        }
    }
}
