using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class LabelUser
    {
        public LabelUser()
        {
            DateFollowed = DateTime.Now;
        }
        [Required]
        public int LabelId { get; set; }
        public Label Label { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime DateFollowed { get; set; }
    }
}
