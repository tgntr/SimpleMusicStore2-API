using System;

namespace SimpleMusicStore.Models.View
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
