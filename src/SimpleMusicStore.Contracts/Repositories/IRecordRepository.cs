using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IRecordRepository : IRepository<Record>
    {
        Task<bool> Exists(int id);
        Task<Record> Find(int id);
    }
}
