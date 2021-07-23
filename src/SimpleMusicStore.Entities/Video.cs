using SimpleMusicStore.Models.Entities;
using System.ComponentModel.DataAnnotations;

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
