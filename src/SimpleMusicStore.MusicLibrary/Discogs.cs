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
        public async Task<RecordInfo> Record(Uri uri)
        {
            var discogsId = await FindId(uri);
            return  await DownloadContent<RecordInfo>(DiscogsConstants.RELEASE, discogsId);
        }

        public async Task<LabelInfo> Label(int id)
        {
            return await DownloadContent<LabelInfo>(DiscogsConstants.LABEL, id);
        }

        public async Task<ArtistInfo> Artist(int id)
        {
            if (id == DiscogsConstants.VARIOUS_ARTISTS_ID)
            {
                return new ArtistInfo { Id = DiscogsConstants.VARIOUS_ARTISTS_ID, Name = DiscogsConstants.VARIOUS_ARTISTS };
            }
            else
            {
                return await DownloadContent<ArtistInfo>(DiscogsConstants.ARTIST, id);
            }
        }

        private async Task<int> FindId(Uri uri)
        {
            if (IsMasterUrl(uri))
                return (await DownloadContent<MasterInfo>(DiscogsConstants.MASTER, uri.FindDiscogsId())).Main_Release;

            return uri.FindDiscogsId();
        }

        private async Task<T> DownloadContent<T>(string contentType, int discogsId)
        {
            var content = await Web().DownloadStringTaskAsync(GenerateUrl(contentType, discogsId));
            //TODO validate
            return JsonConvert.DeserializeObject<T>(content);
        }

        private WebClient Web()
        {
            var web = new WebClient();
            web.AddHeaders();
            return web;
        }

        private bool IsMasterUrl(Uri uri)
        {
            return uri.AbsolutePath.Split(DiscogsConstants.PARAMETER_SPLITTER).Contains(DiscogsConstants.MASTER);
        }

        private string GenerateUrl(string contentType, int discogsId)
        {
            return string.Format(_urlFormat, contentType, discogsId, _key, _secret);
        }
    }
}
