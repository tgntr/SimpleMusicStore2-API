using System;

namespace SimpleMusicStore.Models.View
{
    public class CommentView
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ByUser { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

    }
}
