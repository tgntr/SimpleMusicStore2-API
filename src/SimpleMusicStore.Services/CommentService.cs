using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class CommentService : ICommentsService
    {
        private readonly IUnitOfWork _db;
        private readonly IClaimAccessor _currentUser;
        private readonly IServiceValidator _validator;
        public CommentService(IUnitOfWork db, IClaimAccessor currentUser, IServiceValidator validator)
        {
            _db = db;
            _currentUser = currentUser;
            _validator = validator;
        }


        public async Task<CommentView> Add(NewComment comment)
        {
            comment.UserId = _currentUser.Id;
            var added = await _db.Comments.Add(comment);
            await _db.SaveChanges();
            return added;
        }

        public IEnumerable<CommentView> All(int recordId)
        {
            return _db.Comments.AllFor(recordId);
        }

        public async Task Delete(int commentId)
        {
            if (_validator.IsAuthor(_currentUser.Id, commentId))
            {
                await _db.Comments.Delete(commentId);
                await _db.SaveChanges();
            }


        }
        public async Task<CommentView> Edit(EditComment comment)
        {
            comment.UserId = _currentUser.Id;
            if (_validator.IsAuthor(comment.UserId, comment.Id))
            {
                var edited = await _db.Comments.Edit(comment);
                await _db.SaveChanges();
                return edited;               
            }               
            else
                throw new ArgumentException(ErrorMessages.FORBIDDEN_COMMENT_DELETION);

        }
    }
}
