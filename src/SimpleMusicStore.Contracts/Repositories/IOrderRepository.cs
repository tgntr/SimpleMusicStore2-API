using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task Add(NewOrder order);
        Task<OrderView> Find(int id);
    }
}
