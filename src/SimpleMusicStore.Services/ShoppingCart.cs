using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IDatabase _cartStorage;
        private readonly IRecordRepository _records;
        protected readonly IClaimAccessor _currentUser;
        protected readonly IMapper _mapper;
        protected readonly IServiceValidations _validator;
        protected static IDictionary<int, int> _items;

        public Dictionary<int, int> Items => new Dictionary<int, int>(_items);

        public ShoppingCart(
            IClaimAccessor currentUser,
            IRecordRepository records, 
            IMapper mapper,
            IServiceValidations validator,
            IDatabase cacheProvider)
        {
            _cartStorage = cacheProvider;
            _records = records;
            _currentUser = currentUser;
            _items = FindCurrentUserCart();
            _mapper = mapper;
            _validator = validator;
        }

        public async Task AddToCart(int itemId)
		{
			await _validator.ItemExists(itemId);
			await _validator.ItemIsInStock(itemId);
			await AddItemToShoppingCart(itemId);
			await SaveShoppingCart();
		}

		public async Task DecreaseQuantity(int itemId)
		{
			_validator.ItemIsInCart(itemId);
			DecreaseItemQuantity(itemId);
			await SaveShoppingCart();
		}

		public async Task IncreaseQuantity(int itemId)
		{
            _validator.ItemIsInCart(itemId);
			await _validator.ItemIsInStock(itemId);
			IncreaseItemQuantity(itemId);
			await SaveShoppingCart();
		}
		
		public async Task EmptyCart()
        {
            _items = new Dictionary<int, int>();
            await SaveShoppingCart();
        }
        
        public async Task<ICollection<CartItem>> Cart()
		{
			return await CurrentStateOfCart();
		}
		
		public async Task RemoveFromCart(int itemId)
        {
            _validator.ItemIsInCart(itemId);
            _items.Remove(itemId);
            await SaveShoppingCart();
        }

        public bool IsEmpty() => _items.Count() == 0;

		private async Task<List<CartItem>> CurrentStateOfCart()
		{
			var cart = new List<CartItem>();
			foreach (var item in _items)
			{
				var record = await _records.Find(item.Key);
				var map = _mapper.Map<CartItem>(record);
				map.Quantity = item.Value;
				cart.Add(map);
			}
			return cart;
		}

		private async Task SaveShoppingCart()
        {
            await _cartStorage.StringSetAsync(_currentUser.Id, JsonConvert.SerializeObject(_items));
        }

        private IDatabase RedisDatabase()
        {
            var redisMultiplexer = ConnectionMultiplexer.Connect("localhost");
            return redisMultiplexer.GetDatabase();
        }

        private Dictionary<int, int> FindCurrentUserCart()
        {
            var cart = _cartStorage.StringGet(_currentUser.Id);
            if (string.IsNullOrEmpty(cart))
            {
				return new Dictionary<int, int>();
            }
			return JsonConvert.DeserializeObject<Dictionary<int, int>>(cart);
		}

		private async Task AddItemToShoppingCart(int itemId)
		{
			if (_items.ContainsKey(itemId))
				await IncreaseQuantity(itemId);
			else
				_items[itemId] = 1;
		}

		private void DecreaseItemQuantity(int id)
		{
			if (_items[id] == 1)
				_items.Remove(id);
			else
				_items[id]--;
		}

		private void IncreaseItemQuantity(int itemId)
		{
			_items[itemId]++;
		}
    }
}
