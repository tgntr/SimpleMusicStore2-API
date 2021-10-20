using SimpleMusicStore.Models.View;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IArtistService
    {
        Task<ArtistView> Find(int id);
    }
}
