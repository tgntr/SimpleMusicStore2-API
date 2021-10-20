﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SimpleMusicStore.Data
{
    public class DbRepository<TEntity>
        where TEntity : class
    {
        protected readonly SimpleMusicStoreDbContext _context;
        protected DbSet<TEntity> _set;
        protected IMapper _mapper;

        public DbRepository(SimpleMusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _set = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public DbRepository(SimpleMusicStoreDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        //public Task Add(TEntity entity)
        //{
        //    return _set.AddAsync(entity);
        //}
        //
        //public void Delete(TEntity entity)
        //{
        //    _set.Remove(entity);
        //}
        //
        //public Task SaveChanges()
        //{
        //    return _context.SaveChangesAsync();
        //}
        //
        //public void Dispose()
        //{
        //    _context.Dispose();
        //}
    }
}
