﻿using System;

namespace SimpleMusicStore.Models.View
{
    public class RecordDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public LabelDetails Label { get; set; }
        public ArtistDetails Artist { get; set; }
        public decimal Price { get; set; }
        public int Popularity { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
