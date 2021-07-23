using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ILabelFollowRepository
    {
        Task Add(int labelId, int userId);
        Task<bool> Exists(int labelId, int userId);
        Task Delete(int labelId, int userId);
    }
}
