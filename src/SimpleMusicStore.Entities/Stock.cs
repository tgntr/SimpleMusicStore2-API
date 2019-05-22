using SimpleMusicStore.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class Stock : Entity<int>
    {
        [Required]
        public int RecordId { get; set; }
        public Record Record { get; set; }

        public int Quantity { get; set; }
    }
}
