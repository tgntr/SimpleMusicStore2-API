using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models.Binding
{
    public class NewRecord
    {
        //TODO [DiscogsUrl] move DiscogsUrlAttribute to another project, so there is not a circular dependency
        public string DiscogsUrl { get; set; }

        public decimal Price { get; set; }
    }
}
