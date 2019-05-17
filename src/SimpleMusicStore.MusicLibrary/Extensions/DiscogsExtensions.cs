using Microsoft.Extensions.Configuration;
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

        internal static void AddHeaders(this WebClient client)//, IConfigurationSection credentials)
        {
            client.Headers.Add("user-agent", "SimpleMusicStore2");

            //TODO doesn't authorize 
            //client.Headers.Add("Authorization", $"Discogs key={credentials["key"]} secret={credentials["secret"]}");
        }
    }
}
