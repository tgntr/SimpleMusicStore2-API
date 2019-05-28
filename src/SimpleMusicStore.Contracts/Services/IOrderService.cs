using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IOrderService : ShoppingCart
    {
        
        Task<OrderCheckout> Checkout();
        Task Complete(int addressId);
    }
}
