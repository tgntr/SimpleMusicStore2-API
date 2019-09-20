using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface ICommentsService
    {
        IEnumerable<CommentView> All(int recordId);
        Task<CommentView> Add(NewComment comment);
        Task<CommentView> Edit(EditComment comment);
        Task Delete(int commentId);
    }
}
