using DiscogsUtilities.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

//TODO rename namespaces
namespace DiscogsUtilities
{

    public interface MusicLibrary
    {
        Task<RecordDto> Record(Uri uri);

        Task<LabelDto> Label(int id);

        Task<ArtistDto> Artist(int id);
    }

    public class Discogs : MusicLibrary
    {
        private const string CONTENT_RELEASE = "releases";
        private const string CONTENT_LABEL = "labels";
        private const string CONTENT_ARTIST = "artists";
        private const string CONTENT_MASTER = "masters";
        private const string PARAMETER_SPLITTER = "/";

        private readonly WebClient _web;
        private readonly string _urlFormat = "https://api.discogs.com/{0}/{1}?key={2}&secret={3}";

        public Discogs(WebClient client)
        {
            _web = client;
        }
        //TODO should discogs models be in models project? otherwise how could I access them in the Contracts project?
        public async Task<RecordDto> Record(Uri uri)
        {
            var discogsId = await FindIdAsync(uri);
            return await DownloadContentAsync<RecordDto>(CONTENT_RELEASE, discogsId);
        }

        public async Task<LabelDto> Label(int id)
        {
            return await DownloadContentAsync<LabelDto>(CONTENT_LABEL, id);
        }

        public async Task<ArtistDto> Artist(int id)
        {
            return await DownloadContentAsync<ArtistDto>(CONTENT_ARTIST, id);
        }

        private async Task<int> FindIdAsync(Uri uri)
        {
            if (IsMasterUrl(uri))
                return (await DownloadContentAsync<MasterDto>(CONTENT_MASTER, uri.Id())).Main_Release;

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
