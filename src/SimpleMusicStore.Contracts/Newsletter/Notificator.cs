using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Newsletter
{
    public interface Notificator
    {
        Task Send(string subscriber, string news);
    }
}
