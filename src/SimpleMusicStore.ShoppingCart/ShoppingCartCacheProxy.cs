﻿using AutoMapper;
using Newtonsoft.Json;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Validators;
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
            IServiceValidator validator)
            : base(currentUser, records, mapper, validator)
        {
            _cacheProvider = cacheProvider;
            _items = FindCurrentUserCart();
        }

        public override async Task Add(int itemId)
        {
            await base.Add(itemId);
            await SaveShoppingCart();
        }

        public override async Task Remove(int itemId)
        {
            await base.Remove(itemId);
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

        private Task SaveShoppingCart()
        {
            return _cacheProvider.StringSetAsync(_currentUser.Id.ToString(), JsonConvert.SerializeObject(_items));
        }

        private Dictionary<int, int> FindCurrentUserCart()
        {
            var cart = _cacheProvider.StringGet(_currentUser.Id.ToString());
            if (string.IsNullOrEmpty(cart))
            {
                return new Dictionary<int, int>();
            }
            return JsonConvert.DeserializeObject<Dictionary<int, int>>(cart);
        }
    }
}
