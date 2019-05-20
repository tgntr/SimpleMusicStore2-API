using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class Cart : ICart
    {
        private const string CART = "cart";
        private readonly ISession _session;
        private readonly IRecordService _records;
        private readonly IMapper _mapper;
        private IDictionary<int, int> _items;

        public Cart(IHttpContextAccessor context, IRecordService records, IMapper mapper)
        {
            _session = context.HttpContext.Session;
            _records = records;
            _items = new Dictionary<int, int>();
        }

        public async Task Add(int id)
        {
            await CheckIfValid(id);
            await CheckIfAvailable(id);
            if (_items.ContainsKey(id))
                await IncreaseQuantity(id);
            else
                _items[id] = 1;
            UpdateCart();
        }

        public void DecreaseQuantity(int id)
        {
            CheckIfInCart(id);
            if (_items[id] == 1)
                _items.Remove(id);
            else
                _items[id]--;
            UpdateCart();
        }

        public async Task IncreaseQuantity(int id)
        {
            CheckIfInCart(id);
            await CheckIfAvailable(id);
            _items[id]++;
            UpdateCart();
        }

        public void Empty()
        {
            _items = new Dictionary<int, int>();
            UpdateCart();
        }
        
        public async Task<ICollection<CartItem>> Items()
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
            CheckIfInCart(id);
            _items.Remove(id);
            UpdateCart();
        }


        //todo move these checks to another class???
        private async Task CheckIfValid(int id)
        {
            if (!await _records.Exists(id))
                throw new ArgumentException("invalid record id");
        }

        private void CheckIfInCart(int id)
        {
            if (!_items.ContainsKey(id))
                throw new ArgumentException("cart does not contain such record");
        }

        private async Task CheckIfAvailable(int id)
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
            _session.SetString(CART, JsonConvert.SerializeObject(_items));
        }
    }
}
