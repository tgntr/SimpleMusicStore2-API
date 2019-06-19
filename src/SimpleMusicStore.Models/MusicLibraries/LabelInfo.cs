using SimpleMusicStore.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class LabelInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageInfo[] Images { get; set; }
        public string Image
        {
            get
            {
                if (Images is null || !Images.Any())
                    return DiscogsConstants.DEFAULT_IMAGE;
                else
                    return Images.First().Uri;
            }
        }
    }
}
