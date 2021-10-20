using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Newsletter
{
    public interface NewsletterGenerator
    {
        Task Generate();
    }
}
