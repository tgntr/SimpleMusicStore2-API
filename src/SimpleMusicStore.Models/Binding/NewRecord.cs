using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Models.Binding
{
    public class NewRecord
    {
        //TODO [DiscogsUrl] move DiscogsUrlAttribute to another project, so there is not a circular dependency
        [Required]
        [JsonProperty("discogsUrl")]
        public string DiscogsUrl { get; set; }

        [Required]
        [JsonProperty("price")]
        [Range(1, 100.00, ErrorMessage = "Must be between 1$ and 100$")]
        public decimal Price { get; set; }
    }
}
