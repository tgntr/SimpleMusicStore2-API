using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IOrderService
    {
        Task<ICollection<CartItem>> CurrentCartState();
        Task AddToCart(int itemId);
        Task RemoveFromCart(int itemId);
        Task IncreaseQuantity(int itemId);
        Task DecreaseQuantity(int itemId);
        Task EmptyCart();
        //Task<OrderCheckout> Checkout();
        Task Complete(int addressId);
        Task<OrderView> Find(int orderId);
    }
}
