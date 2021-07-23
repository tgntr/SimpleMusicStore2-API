using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PagedList;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class CommentRepository : DbRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SimpleMusicStoreDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Task Add(NewComment comment)
        {
            return _set.AddAsync(_mapper.Map<Comment>(comment));
        }


        public IEnumerable<CommentView> FindAll(int recordId, int page)
        {
            return _set
                .Where(comment => comment.RecordId == recordId)
                .OrderByDescending(c => c.Date)
                .ToPagedList(page, CommonConstants.PAGE_SIZE)
                .Select(_mapper.Map<CommentView>);
        }
        public async Task Delete(int commentId)
        {
            var toDelete = await _set.FindAsync(commentId);
            if (toDelete != null)
                _set.Remove(toDelete);
            else
                throw new ArgumentException(ErrorMessages.INVALID_COMMENT);

        }

        public async Task Edit(EditComment comment)
        {
            var record = await _set.FindAsync(comment.Id);
            if (record != null)
            {
                _context.Attach(record);
                record.DateEdited = DateTime.Now;
                record.Text = comment.Text;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.INVALID_COMMENT);
            }

        }

        public Task<bool> IsAuthor(int commentId, int userId)
        {
            return _set.AnyAsync(c => c.Id == commentId && c.UserId == userId);
        }
        //private CommentView FillUserName(NewComment comment, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Comment> commentEntity)
        //{
        //    var newComment = _mapper.Map<CommentView>(commentEntity.Entity);
        //    var user = _context.Users.FirstOrDefault(u => u.Id == comment.UserId);
        //    newComment.ByUser = string.Concat(user.Name);
        //    return newComment;
        //}
        //
        //private IEnumerable<CommentView> FillUserNames(List<Comment> comments)
        //{
        //    foreach (var comment in comments)
        //    {
        //        var user = _context.Users.Where(u => u.Id == comment.UserId).FirstOrDefault();
        //        if (user != null)
        //        {
        //            var mappedComment = _mapper.Map<CommentView>(comment);
        //            mappedComment.ByUser = string.Concat(user.Name);
        //            yield return mappedComment;
        //        }
        //    }
        //}

    }
}
