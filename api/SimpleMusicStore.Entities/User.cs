using System;
using System.Collections.Generic;

namespace SimpleMusicStore.Entities
{
    public class User
    {
        public User()
        {
            Addresses = new List<Address>();
            FollowedArtists = new List<ArtistUser>();
            FollowedLabels = new List<LabelUser>();
            Wishlist = new List<Wish>();
            Orders = new List<Order>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<ArtistUser> FollowedArtists { get; set; }

        public ICollection<LabelUser> FollowedLabels { get; set; }

        public ICollection<Wish> Wishlist { get; set; }

        public ICollection<Order> Orders { get; set; }
        
    }
}
