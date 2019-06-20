using SimpleMusicStore.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class Stock : Entity<int>
    {
        public Stock()
        {
            DateAdded = DateTime.UtcNow;
        }

        public Stock(int quantity)
        {
            Quantity = quantity;
        }

        [Required]
        public int RecordId { get; set; }
        public virtual Record Record { get; set; }
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }
    }
}
