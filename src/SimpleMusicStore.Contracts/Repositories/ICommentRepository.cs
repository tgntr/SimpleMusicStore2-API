using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ICommentRepository
    {
        Task<CommentView> Add(NewComment comment);
        Task Delete(int commentId);
        Task<CommentView> Edit(EditComment comment);
        IEnumerable<CommentView> AllFor(int recordId);
        Task<Comment> Get(int commentId);
    }
}
