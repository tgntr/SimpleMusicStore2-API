using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class ArtistUser
    {
        public ArtistUser()
        {
            DateFollowed = DateTime.UtcNow;
        }

        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime DateFollowed { get; set; }
    }
}
