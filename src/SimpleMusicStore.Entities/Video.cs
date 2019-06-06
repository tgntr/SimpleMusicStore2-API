using SimpleMusicStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class Video : Entity<int>
    {
        [Required]
        public string Uri { get; set; }

        [Required]
        public int RecordId { get; set; }
        public virtual Record Record { get; set; }
    }
}
