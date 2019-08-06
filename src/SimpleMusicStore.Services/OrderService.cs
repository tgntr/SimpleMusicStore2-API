using AutoMapper;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using SimpleMusicStore.ShoppingCart;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _db;
        private readonly IShoppingCart _cart;
        private readonly IServiceValidator _validator;
        private readonly IClaimAccessor _currentUser;

        public OrderService(IUnitOfWork db, IShoppingCart cart, IServiceValidator validator, IClaimAccessor currentUser)
        {
            _db = db;
            _cart = cart;
            _validator = validator;
            _currentUser = currentUser;
        }

        public Task AddToCart(int itemId)
        {
            return _cart.Add(itemId);
        }

        public Task<IEnumerable<CartItem>> CurrentCartState()
        {
            return _cart.CurrentState();
        }

        public Task DecreaseQuantity(int itemId)
        {
            return _cart.DecreaseQuantity(itemId);
        }

        public Task EmptyCart()
        {
            return _cart.EmptyCart();
        }

        public Task IncreaseQuantity(int itemId)
        {
            return _cart.IncreaseQuantity(itemId);
        }
        public Task RemoveFromCart(int itemId)
        {
            return _cart.Remove(itemId);
        }

        public Task<OrderView> Find(int orderId)
        {
            return _db.Orders.Find(orderId);
        }

        public async Task Complete(int addressId)
        {
            _validator.CartIsNotEmpty(_cart.Items);
            await _validator.ItemsAreInStock(_cart.Items);
            await _validator.AddressIsValid(addressId);
            await AddNewOrder(addressId);
            await _cart.EmptyCart();
        }

        public IEnumerable<OrderDetails> FindAll()
        {
            //_validator.AccessibleByCurrentUser(userId);
            return _db.Orders.FindAll(_currentUser.Id);
        }

        private async Task AddNewOrder(int addressId)
        {
            var order = new NewOrder
            {
                DeliveryAddressId = addressId,
                UserId = _currentUser.Id,
                Items = _cart.Items
            };

            await _db.Orders.Add(order);
            await _db.SaveChanges();
        }
    }
}
