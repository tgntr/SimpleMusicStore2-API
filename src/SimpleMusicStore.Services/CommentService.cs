using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class CommentService : ICommentsService
    {
        private readonly IUnitOfWork _db;
        private readonly ICurrentUserActivities _currentUser;
        public CommentService(IUnitOfWork db, ICurrentUserActivities currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }


        public async Task Add(NewComment comment)
        {            
            await _db.Comments.Add(comment);
        }

        public IEnumerable<Comment> All(int recordId)
        {
            return _db.Comments.AllFor(recordId);
        }

        public Task Delete(int commentId)
        {
            if (UserIsAuthor(_currentUser.Id, commentId))
                return _db.Comments.Delete(commentId);
            else
                throw new ArgumentException(ErrorMessages.FORBIDDEN_COMMENT_DELETION);
        }
        public async Task Edit(EditComment comment)
        {
            if (comment.UserId == _currentUser.Id)
                await _db.Comments.Edit(comment);
            else
                throw new ArgumentException(ErrorMessages.FORBIDDEN_COMMENT_EDIT);

        }

        private bool UserIsAuthor(string userId, int commentId)
        {
           var currentComment = _db.Comments.Get(commentId).Result;
            if (currentComment.UserId == userId)
                return true;
            else
                return false;
        }

    }
}
