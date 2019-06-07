using SimpleMusicStore.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class Order : UserActivity
    {
        public Order()
			:base()
        {
        }

        public int Id { get; set; }

		//TODO check if works
		public decimal TotalPrice => Items.Sum(i => i.Record.Price);

        [Required]
        public int DeliveryAddressId { get; set; }
        public virtual Address DeliveryAddress { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }
}
