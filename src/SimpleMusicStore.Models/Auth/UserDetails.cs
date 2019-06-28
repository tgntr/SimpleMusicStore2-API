using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models.Auth
{
    public class UserDetails
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
