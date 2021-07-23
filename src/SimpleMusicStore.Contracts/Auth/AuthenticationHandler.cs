using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Auth
{
    public interface AuthenticationHandler
    {
        Task<string> Google(string token);
    }
}
