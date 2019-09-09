using SimpleMusicStore.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Entities
{
    public class Comment : UserActivity
    {
        public Comment() : base()
        {

        }
        
        public int Id { get; set; }
        
        public string Text { get; set; }

        public virtual IList<RecordComment> RecordComments { get; set; }
    }
}
