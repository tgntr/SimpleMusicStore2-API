using SimpleMusicStore.Models.MusicLibraries;
using Newtonsoft.Json;
using SimpleMusicStore.Contracts;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMusicStore.MusicLibrary.Extensions;
using Microsoft.Extensions.Configuration;

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

        //private readonly WebClient _web;
        //TODO hide key and secret best practice
        private readonly string _urlFormat = "https://api.discogs.com/{0}/{1}?key=VpQTKELQqmtSDIXYycSF&secret=cOgmwRrXvdWmubVEeKYYIuZyjyHBaQfr";

        public Discogs(IConfigurationSection credentials)
        {
            
        }
        public async Task<RecordInfo> Record(Uri uri)
        {
            var discogsId = await FindId(uri);
            return  await DownloadContent<RecordInfo>(CONTENT_RELEASE, discogsId);
        }

        public async Task<LabelInfo> Label(int id)
        {
            return await DownloadContent<LabelInfo>(CONTENT_LABEL, id);
        }

        public async Task<ArtistInfo> Artist(int id)
        {
            return await DownloadContent<ArtistInfo>(CONTENT_ARTIST, id);
        }

        private async Task<int> FindId(Uri uri)
        {
            if (IsMasterUrl(uri))
                return (await DownloadContent<MasterInfo>(CONTENT_MASTER, uri.Id())).Main_Release;

            return uri.Id();
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
            return uri.AbsolutePath.Split(PARAMETER_SPLITTER).Contains(CONTENT_MASTER);
        }

        private string GenerateUrl(string contentType, int discogsId)
        {
            return string.Format(_urlFormat, contentType, discogsId);
        }
    }
}
