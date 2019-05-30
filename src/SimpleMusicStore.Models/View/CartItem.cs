using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models.View
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ArtistDto Artist { get; set; }
        public LabelDto Label { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
