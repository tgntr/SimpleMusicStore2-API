using SimpleMusicStore.Models.Binding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface ICommentsService
    {
        IEnumerable<Models.View.Comment> All(int recordId);
        Task Add(NewComment comment);
        Task Edit(EditComment comment);
        Task Delete(int commentId);
    }
}
