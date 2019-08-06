using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
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

        public Task Add(NewOrder order)
        {
            return _set.AddAsync(_mapper.Map<Order>(order));
        }

        public async Task<OrderView> Find(int id)
        {
            var order = await _set.FindAsync(id);
            ValidateThatOrderExists(order);
            return _mapper.Map<OrderView>(order);
        }

        public IEnumerable<OrderDetails> FindAll(string userId)
        {
            return _set.Where(a => a.UserId == userId).Select(_mapper.Map<OrderDetails>);
        }

        private static void ValidateThatOrderExists(Order order)
        {
            if (order == null)
                throw new ArgumentException(ErrorMessages.INVALID_ORDER);
        }
    }
}
