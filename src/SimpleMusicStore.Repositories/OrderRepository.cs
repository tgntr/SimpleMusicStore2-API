using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Repositories
{
    public class OrderRepository : DbRepository<Order>, IOrderRepository
    {
        public OrderRepository(SimpleMusicStoreDbContext db)
            :base(db)
        {
        }
    }
}
