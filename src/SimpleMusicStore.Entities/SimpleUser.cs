using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Models.Entities;
using System;
using System.Collections.Generic;

namespace SimpleMusicStore.Entities
{
    public class SimpleUser : IdentityUser
    {
        public SimpleUser()
        {
            Addresses = new List<Address>();
            FollowedArtists = new List<ArtistFollow>();
            FollowedLabels = new List<LabelFollow>();
            Wishlist = new List<Wish>();
            Orders = new List<Order>();
        }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<ArtistFollow> FollowedArtists { get; set; }
        public ICollection<LabelFollow> FollowedLabels { get; set; }
        public ICollection<Wish> Wishlist { get; set; }
        public ICollection<Order> Orders { get; set; }
        
    }
}
