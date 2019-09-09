using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IRecordCommentRepository
    {
        Task Add(RecordComment comment);
        Task Remove(int commentId);
        IEnumerable<RecordComment> AllBy(int recordId);
    }
}
