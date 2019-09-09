using AutoMapper;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    class RecordCommentRepository : DbRepository<RecordComment>, IRecordCommentRepository
    {
        public RecordCommentRepository(SimpleMusicStoreDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task Add(RecordComment comment)
        {
            await _set.AddAsync(comment);
        }

        public IEnumerable<RecordComment> AllBy(int recordId)
        {
            return _set.Where(rc => rc.RecordId == recordId).ToList();
        }

        public async Task Remove(int commentId)
        {
            var toDelete = await _set.FindAsync(commentId);
            if (toDelete != null)
            {
                _set.Remove(toDelete);
            }
            else
            {
                 throw new ArgumentException(ErrorMessages.RECORD_NOT_IN_WISHLIST);
            }
        }
    }
}
