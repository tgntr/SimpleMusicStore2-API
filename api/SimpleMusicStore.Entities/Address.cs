using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class Address
    {
        public Address()
        {
            Orders = new List<Order>();
            IsActive = true;
        }

        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Country { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string City { get; set; }

        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string Street { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
