using AutoMapper;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class CommentRepository : DbRepository<Entities.Comment>, ICommentRepository
    {
        public CommentRepository(SimpleMusicStoreDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Task Add(Models.View.Comment comment)
        {
           return _set.AddAsync(_mapper.Map<Entities.Comment>(comment));
        }

        public async Task Delete(int commentId)
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

        public async Task<Models.View.Comment> Edit(Models.View.Comment comment)
        {
            var record = await _set.FindAsync(comment.Id);
            record.Date = comment.DateCreated;
            record.Text = comment.Text;
            return _mapper.Map<Models.View.Comment>(record);
           
        }

    }
}
