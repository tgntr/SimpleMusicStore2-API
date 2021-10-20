using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.Binding
{
    public class NewAddress
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        public int UserId { get; set; }
    }
}
