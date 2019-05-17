using SimpleMusicStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class Record : Entity<int>
    {
        public Record()
        {
            Image = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/12in-Vinyl-LP-Record-Angle.jpg/330px-12in-Vinyl-LP-Record-Angle.jpg";
            Videos = new List<Video>();
            Tracks = new List<Track>();
            WantedBy = new List<Wish>();
            Orders = new List<Item>();
            DateAdded = DateTime.UtcNow;
            IsActive = true;
        }


        [Required]
        public string Title { get; set; }

        [Url]
        public string Image { get; set; } 

        //TODO should genre be an entity?
        public string Genre { get; set; }

        [Required]
        public int Year { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }

        //TODO quantity

        public string Format { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [Required]
        public int LabelId { get; set; }
        public Label Label { get; set; }
        
        public ICollection<Wish> WantedBy { get; set; }

        public ICollection<Item> Orders { get; set; }

        public ICollection<Video> Videos { get; set; }

        public ICollection<Track> Tracks { get; set; }

    }
}
