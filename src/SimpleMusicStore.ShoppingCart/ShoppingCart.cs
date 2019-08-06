using AutoMapper;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleMusicStore.ShoppingCart
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IRecordRepository _records;
        protected readonly IClaimAccessor _currentUser;
        protected readonly IMapper _mapper;
        protected readonly IServiceValidator _validator;
        protected IDictionary<int, int> _items;

        public Dictionary<int, int> Items => new Dictionary<int, int>(_items);

        public ShoppingCart(IClaimAccessor currentUser,
            IRecordRepository records,
            IMapper mapper,
            IServiceValidator validator)
        {
            _records = records;
            _currentUser = currentUser;
            _mapper = mapper;
            _validator = validator;
        }

        public virtual async Task Add(int itemId)
        {
            await _validator.RecordExists(itemId);
            await _validator.ItemIsInStock(itemId, Items);
            await AddItemToShoppingCart(itemId);
        }

        public virtual async Task DecreaseQuantity(int itemId)
        {
            _validator.ItemIsInCart(itemId, Items);
            await Task.Run(() => DecreaseItemQuantity(itemId));
        }

        public virtual async Task IncreaseQuantity(int itemId)
        {
            _validator.ItemIsInCart(itemId, Items);
            await _validator.ItemIsInStock(itemId, Items);
            IncreaseItemQuantity(itemId);
        }

        public virtual async Task EmptyCart()
        {
            await Task.Run(() => _items = new Dictionary<int, int>());
        }

        public virtual async Task<IEnumerable<CartItem>> CurrentState()
        {
            return await CurrentStateOfCart();
        }

        public virtual async Task Remove(int itemId)
        {
            _validator.ItemIsInCart(itemId, Items);
            await Task.Run(() => _items.Remove(itemId));
        }

        private async Task<List<CartItem>> CurrentStateOfCart()
        {
            var cart = new List<CartItem>();
            foreach (var item in _items)
            {
                var cartItem = _mapper.Map<CartItem>(await _records.Find(item.Key));
                cartItem.Quantity = item.Value;
                cart.Add(cartItem);
            }
            return cart;
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
