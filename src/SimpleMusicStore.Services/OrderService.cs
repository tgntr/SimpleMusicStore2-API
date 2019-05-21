using AutoMapper;
using Microsoft.AspNetCore.Http;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    class OrderService : IOrderService
    {
        private readonly ICart _cart;
        private readonly IAddressService _addresses;
        private readonly string _currentUserId;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orders;

        public OrderService(ICart cart, IAddressService addresses, IHttpContextAccessor httpContext, IMapper mapper, IOrderRepository orders)
        {
            //TODO is it a good approach ???
            _cart = cart;
            //todo work with address service when available
            _addresses = addresses;
            _currentUserId = httpContext.HttpContext.User.FindFirstValue("id");
            _mapper = mapper;
            _orders = orders;
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
            return await _cart.Current();
        }

        public void RemoveFromCart(int id)
        {
            _cart.Remove(id);
        }

        public void EmptyCart()
        {
            _cart.Empty();
        }

        public async Task<OrderCheckout> Checkout()
        {
            CheckIfCartIsEmpty();
            return new OrderCheckout
            {
                Addresses = await _addresses.FindAll(_currentUserId),
                Items = await _cart.Current()
            };
        }

        public async Task Finish(int addressId)
        {
            CheckIfCartIsEmpty();
            await CheckIfAddressIsValid(addressId);

            var order = new Order
            {
                DeliveryAddressId = addressId,
                UserId = _currentUserId,
                Items = _cart.Items.Select(i => _mapper.Map<Item>(i)).ToList()
            };
            await _orders.Add(order);
            await _orders.SaveChanges();

            
            //TODO _records.DecreaseQuantities(_cart.Items);
            EmptyCart();

        }

        private async Task CheckIfAddressIsValid(int id)
        {
            //todo check if it's current user's address
            if(!await _addresses.Exists(id, _currentUserId))
            {
                throw new ArgumentException("invalid address");
            }
        }

        private void CheckIfCartIsEmpty()
        {
            if (_cart.IsEmpty())
                throw new OperationCanceledException("cart is empty");
        }
    }
}
