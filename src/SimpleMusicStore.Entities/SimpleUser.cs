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

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ArtistFollow> FollowedArtists { get; set; }
        public virtual ICollection<LabelFollow> FollowedLabels { get; set; }
        public virtual ICollection<Wish> Wishlist { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        
    }
}
