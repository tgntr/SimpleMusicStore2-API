using AutoMapper;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using SimpleMusicStore.ShoppingCart;
using StackExchange.Redis;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class OrderService : ShoppingCartCacheProxy, IOrderService
    {
        private readonly IAddressRepository _addresses;
        private readonly IOrderRepository _orders;

        public OrderService(
            IAddressRepository addresses,
            IMapper mapper,
            IOrderRepository orders,
            IRecordRepository records,
            IServiceValidator validator,
            IClaimAccessor currentUser,
            IDatabase cacheProvider)
            : base(currentUser, records, mapper, cacheProvider, validator)
        {
            _addresses = addresses;
            _orders = orders;
        }

        public async Task<OrderCheckout> Checkout()
        {
            _validator.CartIsNotEmpty(Items);
            return await GenerateOrderCheckoutDetailsView();
        }

        public async Task Complete(int addressId)
        {
            _validator.CartIsNotEmpty(Items);
            await _validator.AddressIsValid(addressId);
            await AddNewOrder(addressId);
            await EmptyCart();
        }

        public async Task<OrderView> Find(int orderId)
        {
            return await _orders.Find(orderId);
        }

        private async Task<OrderCheckout> GenerateOrderCheckoutDetailsView()
        {
            return new OrderCheckout
            {
                Addresses =  _addresses.FindAll(_currentUser.Id),
                Items = await Cart()
            };
        }

        private async Task AddNewOrder(int addressId)
        {
            var order = new Entities.Order
            {
                DeliveryAddressId = addressId,
                UserId = _currentUser.Id,
                Items = Items.Select(i => _mapper.Map<Item>(i)).ToList()
            };

            await _orders.Add(order);
            await _orders.SaveChanges();
        }
    }
}
