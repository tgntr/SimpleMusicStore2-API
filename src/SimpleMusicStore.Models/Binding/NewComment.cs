using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Models.Binding
{
    public class NewComment
    {
        [Required]
        public string UserId { get; set; }
        [MinLength(1), Required]
        public string Text { get; set; }
        [Required]
        public int RecordId { get; set; }
    }
}
