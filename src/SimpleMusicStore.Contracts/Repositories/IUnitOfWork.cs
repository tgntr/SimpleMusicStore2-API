using System;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository Addresses { get; }
        IArtistFollowRepository ArtistFollows { get; }
        IArtistRepository Artists { get; }
        ILabelFollowRepository LabelFollows { get; }
        ILabelRepository Labels { get; }
        IOrderRepository Orders { get; }
        IRecordRepository Records { get; }
        IWishRepository Wishes { get; }
        IUserRepository Users { get; }
        IStockRepository Stocks { get; }
        ICommentRepository Comments { get; }
        Task SaveChanges();
    }
}
