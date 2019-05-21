using AutoMapper;
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
        protected static readonly ICollection<TEntity> _set = new List<TEntity>();
        protected readonly IMapper _mapper;

        public ListRepository()
        {

        }

        public ListRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task Add(TEntity entity)
        {
            return Task.Run(() => _set.Add(entity));
        }

        public IEnumerable<TEntity> All()
        {
            return _set;
        }

        public void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }

        public Task<int> SaveChanges()
        {
            return null;
        }

    }
}
