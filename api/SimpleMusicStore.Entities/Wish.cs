using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class Wish
    {
        public Wish()
        {
            DateFollowed = DateTime.UtcNow;
        }
        [Required]
        public int RecordId { get; set; }
        public Record Record { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime DateFollowed { get; set; }
    }
}
