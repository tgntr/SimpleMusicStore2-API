using Newtonsoft.Json;
using System;
using System.Text;

namespace SimpleMusicStore.Models.AuthenticationProviders
{
    [JsonObject("JwtToken")]
    public class JwtTokenConfiguration
    {
        [JsonProperty("Secret")]
        public string Secret { get; set; }

        [JsonProperty("Issuer")]
        public string Issuer { get; set; }

        [JsonProperty("Audience")]
        public string Audience { get; set; }

        [JsonProperty("AccessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("RefreshExpiration")]
        public int RefreshExpiration { get; set; }

        public byte[] SecretEncoded()
        {
            return Encoding.UTF8.GetBytes(Secret);
        }

    }
}
