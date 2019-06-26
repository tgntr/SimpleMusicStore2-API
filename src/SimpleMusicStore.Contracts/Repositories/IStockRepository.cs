using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IStockRepository
    {
        Task Add(int recordId, int quantity);
    }
}
