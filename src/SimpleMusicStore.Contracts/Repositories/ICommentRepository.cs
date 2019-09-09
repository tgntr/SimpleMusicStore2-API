using SimpleMusicStore.Models.View;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ICommentRepository
    {
        Task Add(Comment comment);
        Task Delete(int commentId);
        Task<Comment> Edit(Comment comment);
    }
}
