using AutoMapper;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
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

        public async Task Add(NewComment comment)
        {
            var comm = await _set.AddAsync(_mapper.Map<Entities.Comment>(comment));
            FillUserName(comment, comm);
            await _context.SaveChangesAsync();

        }


        public IEnumerable<Models.View.Comment> AllFor(int recordId)
        {
            var comments = _set.Where(comment => comment.RecordId == recordId).ToList();
            return FillUserNames(comments);
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
                throw new ArgumentException(ErrorMessages.INVALID_COMMENT);
            }

        }

        public async Task<Models.View.Comment> Edit(EditComment comment)
        {
            var record = await _set.FindAsync(comment.Id);
            if (record != null)
            {
                _context.Attach(record);
                record.Date = DateTime.Now;
                record.Text = comment.Text;
                _context.SaveChanges();
                return _mapper.Map<Models.View.Comment>(record);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.INVALID_COMMENT);
            }

        }

        public async Task<Entities.Comment> Get(int commentId)
        {
           return await _set.FindAsync(commentId);
        }
        private void FillUserName(NewComment comment, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entities.Comment> comm)
        {
            var newComment = _mapper.Map<Models.View.Comment>(comm.Entity);
            var user = _context.Users.FirstOrDefault(u => u.Id == comment.UserId);
            newComment.ByUser = string.Concat(user.FirstName, " ", user.LastName);
        }

        private IEnumerable<Models.View.Comment> FillUserNames(List<Entities.Comment> comments)
        {
            foreach (var comment in comments)
            {
                var user = _context.Users.Where(u => u.Id == comment.UserId).FirstOrDefault();
                if (user != null)
                {
                    var mappedComment = _mapper.Map<Models.View.Comment>(comment);
                    mappedComment.ByUser = string.Concat(user.FirstName, " ", user.LastName);
                    yield return mappedComment;
                }
            }
        }

    }
}
