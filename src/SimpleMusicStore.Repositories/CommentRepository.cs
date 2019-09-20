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
    public class CommentRepository : DbRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SimpleMusicStoreDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task<CommentView> Add(NewComment comment)
        {
            var newComment = await _set.AddAsync(_mapper.Map<Comment>(comment));
            return FillUserName(comment, newComment);
        }


        public IEnumerable<CommentView> AllFor(int recordId)
        {
            var comments = _set.Where(comment => comment.RecordId == recordId).ToList();
            return FillUserNames(comments);
        }
        public async Task Delete(int commentId)
        {
            var toDelete = await _set.FindAsync(commentId);
            if (toDelete != null)
                _set.Remove(toDelete);
            else
                throw new ArgumentException(ErrorMessages.INVALID_COMMENT);

        }

        public async Task<CommentView> Edit(EditComment comment)
        {
            var record = await _set.FindAsync(comment.Id);
            if (record != null && record.UserId == comment.UserId)
            {
                _context.Attach(record);
                record.DateEdited = DateTime.Now;
                record.Text = comment.Text;
                return _mapper.Map<CommentView>(record);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.INVALID_COMMENT);
            }

        }

        public async Task<Comment> Get(int commentId)
        {
            return await _set.FindAsync(commentId);
        }
        private CommentView FillUserName(NewComment comment, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Comment> commentEntity)
        {
            var newComment = _mapper.Map<CommentView>(commentEntity.Entity);
            var user = _context.Users.FirstOrDefault(u => u.Id == comment.UserId);
            newComment.ByUser = string.Concat(user.FirstName, " ", user.LastName);
            return newComment;
        }

        private IEnumerable<CommentView> FillUserNames(List<Comment> comments)
        {
            foreach (var comment in comments)
            {
                var user = _context.Users.Where(u => u.Id == comment.UserId).FirstOrDefault();
                if (user != null)
                {
                    var mappedComment = _mapper.Map<CommentView>(comment);
                    mappedComment.ByUser = string.Concat(user.FirstName, " ", user.LastName);
                    yield return mappedComment;
                }
            }
        }

    }
}
