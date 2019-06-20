using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class OrderRepository : DbRepository<Order>, IOrderRepository
    {
        public OrderRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            :base(db, mapper)
        {
        }

        public async Task<OrderView> Find(int id)
        {
            return _mapper.Map<OrderView>(await _set.FindAsync(id));
        }

        public Task<bool> Exists(int orderId, string userId)
        {
            return _set.AnyAsync(o => o.Id == orderId && o.UserId == userId);
        }
    }
}
