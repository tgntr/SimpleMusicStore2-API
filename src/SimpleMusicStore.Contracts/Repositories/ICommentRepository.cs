using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ICommentRepository
    {
        Task Add(NewComment comment);
        Task Delete(int commentId);
        Task<Comment> Edit(EditComment comment);
        IEnumerable<Comment> AllFor(int recordId);
        Task<Entities.Comment> Get(int commentId);
    }
}
