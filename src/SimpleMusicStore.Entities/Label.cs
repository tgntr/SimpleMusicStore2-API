using SimpleMusicStore.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class Label : Entity<int>
    {
        public Label()
        {
            Image = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/12in-Vinyl-LP-Record-Angle.jpg/330px-12in-Vinyl-LP-Record-Angle.jpg";
            Records = new List<Record>();
            Followers = new List<LabelFollow>();

        }

        [Required]
        public string Name { get; set; }

        [Url]
        public string Image { get; set; } 

        public virtual ICollection<Record> Records { get; set; }

        public virtual ICollection<LabelFollow> Followers { get; set; }

    }
}
