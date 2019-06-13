using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IOrderService : IShoppingCart
    {
        Task<OrderCheckout> Checkout();
        Task Complete(int addressId);
        Task<OrderView> Find(int orderId);
    }
}
