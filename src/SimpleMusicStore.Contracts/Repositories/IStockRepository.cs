using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IStockRepository
    {
        Task Add(int recordId, int quantity);
    }
}
