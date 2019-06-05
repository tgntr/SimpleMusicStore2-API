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
        private readonly SimpleMusicStoreDbContext context;
        protected DbSet<TEntity> _set;

        public DbRepository(SimpleMusicStoreDbContext context)
        {
            this.context = context;
            this._set = this.context.Set<TEntity>();
        }

        public Task Add(TEntity entity)
        {
            return this._set.AddAsync(entity);
        }

        public IEnumerable<TEntity> All()
        {
            return this._set;
        }

        public void Delete(TEntity entity)
        {
            this._set.Remove(entity);
        }

        public Task<int> SaveChanges()
        {
            return this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
