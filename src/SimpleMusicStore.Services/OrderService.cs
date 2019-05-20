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


        public async Task AddToCart(int id)
        {
            await _cart.Add(id);
        }


        public void DecreaseQuantity(int id)
        {
            //todo should i make this async anyway??
            _cart.DecreaseQuantity(id);
        }

        public async Task IncreaseQuantity(int id)
        {
            await _cart.IncreaseQuantity(id);
        }

        public async Task<ICollection<CartItem>> Cart()
        {
            return await _cart.Items();
        }

        public void RemoveFromCart(int id)
        {
            _cart.Remove(id);
        }

        public void EmptyCart()
        {
            _cart.Empty();
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
