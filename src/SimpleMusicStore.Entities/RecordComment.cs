using SimpleMusicStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class RecordComment : Entity<int>
    {

        public int RecordId { get; set; }
        public virtual Record Record { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
