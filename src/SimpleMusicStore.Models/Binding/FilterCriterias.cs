using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models.Binding
{
    public class FilterCriterias
    {
        public FilterCriterias()
        {
            Genres = new List<string>();
            Formats = new List<string>();
        }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Formats { get; set; }
    }
}
