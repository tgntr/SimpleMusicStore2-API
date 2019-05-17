using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface ICart
    {
        Task<ICollection<CartItem>> Items();
        Task Add(int id);
        Task Remove(int id);
        Task IncreaseQuantity(int id);
        Task DecreaseQuantity(int id);
        Task EmptyCart();
    }
}
