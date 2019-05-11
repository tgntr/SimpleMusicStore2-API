using SimpleMusicStore.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class Wish : UserActivity
    {
        public Wish()
			:base()
        {
        }

        [Required]
        public int RecordId { get; set; }
        public Record Record { get; set; }

    }
}
