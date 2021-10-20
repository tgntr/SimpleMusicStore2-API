using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts
{
    public interface FileStorage
    {
        Task Upload(IFormFile file, string fileName);
    }
}
