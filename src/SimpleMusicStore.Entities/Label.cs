using SimpleMusicStore.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class Label : Entity<int>
    {
        public Label()
        {
            ImageUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/12in-Vinyl-LP-Record-Angle.jpg/330px-12in-Vinyl-LP-Record-Angle.jpg";
            Records = new List<Record>();
            Followers = new List<LabelFollow>();

        }

        [Required]
        public int DiscogsId { get; set; }

        [Required]
        public string Name { get; set; }

        [Url]
        public string ImageUrl { get; set; } 

        public string Description { get; set; }

        public ICollection<Record> Records { get; set; }

        public ICollection<LabelFollow> Followers { get; set; }

    }
}
