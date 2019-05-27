using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
    public class ShoppingCart : ICart
    {
        private const string CART = "cart";

        private readonly IDatabase _cartStorage;
        private readonly IRecordService _records;
        private readonly string _currentUserId;
        private readonly IMapper _mapper;
        private IDictionary<int, int> _items;

        public ShoppingCart(IHttpContextAccessor context, IRecordService records, IMapper mapper)
        {
            _cartStorage = RedisDatabase();
            _records = records;
            _currentUserId = context.HttpContext.User.FindFirstValue("id");
            _items = FindCartItems(_currentUserId);
            _mapper = mapper;
        }

        public IDictionary<int, int> Items => _items;

        public async Task Add(int itemId)
        {
            await ValidateThatItemExists(itemId);
            await ValidateThatItemIsInStock(itemId);
            if (_items.ContainsKey(itemId))
                await IncreaseQuantity(itemId);
            else
                _items[itemId] = 1;
            UpdateCart();
        }

        public void DecreaseQuantity(int id)
        {
            ValidateThatItemIsInCart(id);
            if (_items[id] == 1)
                _items.Remove(id);
            else
                _items[id]--;
            UpdateCart();
        }

        public async Task IncreaseQuantity(int itemId)
        {
            ValidateThatItemIsInCart(itemId);
            await ValidateThatItemIsInStock(itemId);
            _items[itemId]++;
            UpdateCart();
        }

        public void Empty()
        {
            _items = new Dictionary<int, int>();
            UpdateCart();
        }
        
        public async Task<ICollection<CartItem>> Current()
        {
            //todo a better way???
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

        public void Remove(int id)
        {
            ValidateThatItemIsInCart(id);
            _items.Remove(id);
            UpdateCart();
        }

        public bool IsEmpty() => _items.Count() == 0;
        //todo move these checks to another class???
        private async Task ValidateThatItemExists(int id)
        {
            if (!await _records.Exists(id))
                throw new ArgumentException("invalid record id");
        }

        private void ValidateThatItemIsInCart(int id)
        {
            if (!_items.ContainsKey(id))
                throw new ArgumentException("cart does not contain such record");
        }

        private async Task ValidateThatItemIsInStock(int id)
        {
            //todo better way to check an item that is about to be added
            var quantity = 0;
            if (_items.ContainsKey(id))
                quantity = _items[id];

            if (await _records.Availability(id) <= quantity)
            {
                throw new ArgumentException("required quantity is not available");
            }
        }

        private void UpdateCart()
        {
            _cartStorage.StringSet(CART, JsonConvert.SerializeObject(_items));
        }

        private IDatabase RedisDatabase()
        {
            var redisMultiplexer = ConnectionMultiplexer.Connect("localhost");
            return redisMultiplexer.GetDatabase();
        }

        private Dictionary<int, int> FindCartItems(string userId)
        {
            var cart = _cartStorage.StringGet(_currentUserId);
            if (!string.IsNullOrEmpty(cart))
            {
                return JsonConvert.DeserializeObject<Dictionary<int, int>>(cart);
            }
            return new Dictionary<int, int>();
        }
    }
}
