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
        private readonly IMapper _mapper;

        public OrderRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            :base(db)
        {
            _mapper = mapper;
        }

        public async Task<OrderView> Find(int id)
        {
            var order = await _set.FirstAsync(o => o.Id == id);
            return _mapper.Map<OrderView>(order);
        }

        public Task<bool> Exists(int orderId, string userId)
        {
            return _set.AnyAsync(o => o.Id == orderId && o.UserId == userId);
        }
    }
}
