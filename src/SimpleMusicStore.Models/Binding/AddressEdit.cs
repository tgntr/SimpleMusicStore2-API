using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.Binding
{
    public class AddressEdit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
    }
}
