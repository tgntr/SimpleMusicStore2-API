using SimpleMusicStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class Track : Entity<int>
    {
        [Required]
        public string Title { get; set; }
        public string Duration { get; set; }
        [Url]
        public string Preview { get; set; }
        [Required]
        public int RecordId { get; set; }
        public virtual Record Record { get; set; }
       
    }
}
