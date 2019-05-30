using AutoMapper;
using Microsoft.AspNetCore.Http;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    class OrderService : ShoppingCart, IOrderService
    {
        private readonly IAddressRepository _addresses;
        private readonly IOrderRepository _orders;

        public OrderService(
            IAddressRepository addresses,
            IMapper mapper,
            IOrderRepository orders,
            IRecordRepository records,
            IServiceValidations validator,
            IClaimAccessor currentUser)
            : base(currentUser, records, mapper, validator)
        {
            _addresses = addresses;
            _orders = orders;
        }

        public async Task<OrderCheckout> Checkout()
        {
            _validator.CartIsNotEmpty();
            return await GenerateOrderCheckoutDetailsView();
        }

        public async Task Complete(int addressId)
        {
            _validator.CartIsNotEmpty();
            await _validator.AddressIsValid(addressId);
            await AddNewOrder(addressId);
            await EmptyCart();
        }

        private async Task<OrderCheckout> GenerateOrderCheckoutDetailsView()
        {
            return new OrderCheckout
            {
                Addresses = await _addresses.FindAll(_currentUser.Id),
                Items = await Cart()
            };
        }

        private async Task AddNewOrder(int addressId)
        {
            Order order = new Order
            {
                DeliveryAddressId = addressId,
                UserId = _currentUser.Id,
                Items = _items.Select(i => _mapper.Map<Item>(i)).ToList()
            };

            await _orders.Add(order);
            await _orders.SaveChanges();
        }
    }
}
