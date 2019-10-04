﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models.View
{
    public class CommentView
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ByUser { get; set; }
        //public string UserId { get; set; }
        public DateTime Date { get; set; }
        public bool IsAuthor { get; set; }

    }
}
