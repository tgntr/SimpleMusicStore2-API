using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.UtcNow;
        }
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }


        [Required]
        public int DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<RecordOrder> Items { get; set; }

    }
}
