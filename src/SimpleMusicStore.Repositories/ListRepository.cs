using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class ListRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        //private readonly FunAppContext context;
        protected static readonly ICollection<TEntity> set = new List<TEntity>();

        public Task Add(TEntity entity)
        {
            return Task.Run(() => set.Add(entity));
        }

        public IEnumerable<TEntity> All()
        {
            return set;
        }

        public void Delete(TEntity entity)
        {
            set.Remove(entity);
        }

        public Task<int> SaveChanges()
        {
            return null;
        }

    }
}
