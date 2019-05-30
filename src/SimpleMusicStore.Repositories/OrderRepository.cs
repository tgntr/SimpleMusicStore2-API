using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Repositories
{
    public class OrderRepository : ListRepository<Order>, IOrderRepository
    {
    }
}
