using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    class OrderService : IOrderService
    {
        private readonly ICart _cart;
        public OrderService(ICart cart)
        {
            //TODO is it a good approach ???
            _cart = cart;
        }


        public Task AddToCart(int id)
        {
            throw new NotImplementedException();
        }


        public Task DecreaseQuantity(int id)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseQuantity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CartItem>> Cart()
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromCart(int id)
        {
            throw new NotImplementedException();
        }

        public Task EmptyCart()
        {
            throw new NotImplementedException();
        }

        public Task<OrderCheckout> Checkout()
        {
            throw new NotImplementedException();
        }

        public Task Finish(int addressId)
        {
            throw new NotImplementedException();
        }
    }
}
