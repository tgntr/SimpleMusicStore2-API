using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMusicStore.Models.View
{
    public class OrderCheckout
    {
        public IEnumerable<AddressDto> Addresses { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);
    }
}
