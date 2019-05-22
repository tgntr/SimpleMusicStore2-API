using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IOrderService
    {
        Task<ICollection<CartItem>> Cart();
        Task AddToCart(int id);
        void RemoveFromCart(int id);
        Task IncreaseQuantity(int id);
        void DecreaseQuantity(int id);
        void EmptyCart();
        Task<OrderCheckout> Checkout();
        Task Complete(int addressId);
    }
}
