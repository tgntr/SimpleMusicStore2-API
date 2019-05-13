using SimpleMusicStore.Models.MusicLibraries;
using Newtonsoft.Json;
using SimpleMusicStore.Contracts;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMusicStore.MusicLibrary.Extensions;
using SimpleMusicStore.Contracts;

//TODO rename namespaces
namespace SimpleMusicStore.MusicLibrary
{
    public class Discogs : MusicSource
    {
		private const string
			CONTENT_RELEASE = "releases",
			CONTENT_LABEL = "labels",
			CONTENT_ARTIST = "artists",
			CONTENT_MASTER = "masters",
			PARAMETER_SPLITTER = "/";

        private readonly WebClient _web;
        private readonly string _urlFormat = "https://api.discogs.com/{0}/{1}?key={2}&secret={3}";

        public Discogs(WebClient client)
        {
            _web = client;
        }
        //TODO should discogs models be in models project? otherwise how could I access them in the Contracts project?
        public async Task<RecordInfo> Record(Uri uri)
        {
            var discogsId = await FindIdAsync(uri);
            return await DownloadContentAsync<RecordInfo>(CONTENT_RELEASE, discogsId);
        }

        public async Task<LabelInfo> Label(int id)
        {
            return await DownloadContentAsync<LabelInfo>(CONTENT_LABEL, id);
        }

        public async Task<ArtistInfo> Artist(int id)
        {
            return await DownloadContentAsync<ArtistInfo>(CONTENT_ARTIST, id);
        }

        private async Task<int> FindIdAsync(Uri uri)
        {
            if (IsMasterUrl(uri))
                return (await DownloadContentAsync<MasterInfo>(CONTENT_MASTER, uri.Id())).Main_Release;

            return uri.Id();
        }

        private async Task<T> DownloadContentAsync<T>(string contentType, int discogsId)
        {
            var content = await _web.DownloadStringTaskAsync(GenerateUrl(contentType, discogsId));
            //TODO validate
            return JsonConvert.DeserializeObject<T>(content);
        }

        private bool IsMasterUrl(Uri uri)
        {
            return uri.AbsolutePath.Split(PARAMETER_SPLITTER).Contains(CONTENT_MASTER);
        }

        private string GenerateUrl(string contentType, int discogsId)
        {
            return string.Format(_urlFormat, contentType, discogsId);
        }
    }
}
