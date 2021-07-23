using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.Binding
{
    public class NewComment
    {
        public int UserId { get; set; }
        [MinLength(1), Required]
        public string Text { get; set; }
        [Required]
        public int RecordId { get; set; }
    }
}
