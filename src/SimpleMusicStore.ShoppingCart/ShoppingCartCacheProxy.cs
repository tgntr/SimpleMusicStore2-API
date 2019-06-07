using AutoMapper;
using Newtonsoft.Json;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.ShoppingCart
{
    public class ShoppingCartCacheProxy : ShoppingCart
    {
        private readonly IDatabase _cacheProvider;

        public ShoppingCartCacheProxy(
            IClaimAccessor currentUser,
            IRecordRepository records,
            IMapper mapper,
            IDatabase cacheProvider,
            IServiceValidations validator)
            : base(currentUser, records, mapper, validator)
        {
            _cacheProvider = cacheProvider;
            _items = FindCurrentUserCart();
        }

        public override async Task AddToCart(int itemId)
        {
            await base.AddToCart(itemId);
            await SaveShoppingCart();
        }

        public override async Task RemoveFromCart(int itemId)
        {
            await base.RemoveFromCart(itemId);
            await SaveShoppingCart();
        }

        public override async Task EmptyCart()
        {
            await base.EmptyCart();
            await SaveShoppingCart();
        }

        public override async Task IncreaseQuantity(int itemId)
        {
            await base.IncreaseQuantity(itemId);
            await SaveShoppingCart();
        }
        public override async Task DecreaseQuantity(int itemId)
        {
            await base.DecreaseQuantity(itemId);
            await SaveShoppingCart();
        }

        private async Task SaveShoppingCart()
        {
            await _cacheProvider.StringSetAsync(_currentUser.Id, JsonConvert.SerializeObject(_items));
        }

        private Dictionary<int, int> FindCurrentUserCart()
        {
            var cart = _cacheProvider.StringGet(_currentUser.Id);
            if (string.IsNullOrEmpty(cart))
            {
                return new Dictionary<int, int>();
            }
            return JsonConvert.DeserializeObject<Dictionary<int, int>>(cart);
        }
    }
}
