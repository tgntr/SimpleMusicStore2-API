using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<OrderView> Find(int id);
    }
}
