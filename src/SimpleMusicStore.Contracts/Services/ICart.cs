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
        void Remove(int id);
        Task IncreaseQuantity(int id);
        void DecreaseQuantity(int id);
        void Empty();
    }
}
