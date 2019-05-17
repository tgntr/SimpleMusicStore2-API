using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IOrderService
    {
        Task<ICollection<CartItem>> Cart();
        Task AddToCart(int id);
        Task RemoveFromCart(int id);
        Task IncreaseQuantity(int id);
        Task DecreaseQuantity(int id);
        Task EmptyCart();
        Task<OrderCheckout> Checkout();
        Task Finish(int addressId);
    }
}
