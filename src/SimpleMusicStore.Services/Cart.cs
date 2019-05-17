using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class Cart : ICart
    {
        public Task Add(int id)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseQuantity(int id)
        {
            throw new NotImplementedException();
        }

        public Task EmptyCart()
        {
            throw new NotImplementedException();
        }

        public Task IncreaseQuantity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CartItem>> Items()
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
