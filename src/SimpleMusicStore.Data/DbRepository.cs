using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly SimpleMusicStoreDbContext _context;
        protected DbSet<TEntity> _set;

        public DbRepository(SimpleMusicStoreDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public Task Add(TEntity entity)
        {
            return _set.AddAsync(entity);
        }

        public IEnumerable<TEntity> All()
        {
            return _set;
        }

        public void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
