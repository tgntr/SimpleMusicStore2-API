using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities.Common
{
    public abstract class UserActivity
    {
        public UserActivity()
        {
            Date = DateTime.UtcNow;
        }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime Date { get; set; }
    }
}
