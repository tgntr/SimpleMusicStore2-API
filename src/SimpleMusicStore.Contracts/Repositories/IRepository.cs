using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {

        Task Add(TEntity entity);

        void Delete(TEntity entity);

        Task SaveChanges();
    }
}
