using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IUserService
    {
        Task Add(ClaimsPrincipal user);
    }
}
