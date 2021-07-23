using SimpleMusicStore.Entities.Common;
using System;

namespace SimpleMusicStore.Entities
{
    public class Comment : UserActivity
    {
        public Comment() : base()
        {

        }
        public DateTime DateEdited { get; set; }
        public int Id { get; set; }

        public string Text { get; set; }

        public int RecordId { get; set; }
        public virtual Record Record { get; set; }
    }
}
