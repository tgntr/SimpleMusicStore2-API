using SimpleMusicStore.Models.MusicLibraries;
using System;
using System.Linq;
using System.Net;

namespace SimpleMusicStore.MusicLibrary.Extensions
{
    public static class DiscogsExtensions
    {
        internal static int Id(this Uri uri)
        {
            var id = uri.AbsolutePath.Split("/").Last();

            return int.Parse(id);
        }

        internal static void AddHeaders(this WebClient client)
        {
            //TODO pass key and secret
            client.Headers.Add("user-agent", "SimpleMusicStore");
            client.Headers.Add("Authorization", "Discogs key=MY_CONSUMER_KEY secret=MY_CONSUMER_SECRET");
        }
    }
}
