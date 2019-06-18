using Newtonsoft.Json;
using SimpleMusicStore.Constants;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.Binding
{
    public class NewRecord
    {
        //TODO [DiscogsUrl] move DiscogsUrlAttribute to another project, so there is not a circular dependency
        [Required]
        [RegularExpression(DiscogsConstants.DISCOGS_URL_PATTERN)]
        [JsonProperty("discogsUrl")]
        public string DiscogsUrl { get; set; }

        [Required]
        [JsonProperty("price")]
        [Range(1, 100.00, ErrorMessage = ErrorMessages.PRICE_LIMIT)]
        public decimal Price { get; set; }
    }
}
