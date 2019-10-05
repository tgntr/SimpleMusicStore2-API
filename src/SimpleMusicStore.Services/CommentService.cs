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
	public class CommentService : ICommentService
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


		public async Task Add(NewComment comment)
		{
			comment.UserId = _currentUser.Id;
			await _db.Comments.Add(comment);
			await _db.SaveChanges();
		}

		public IEnumerable<CommentView> FindAll(int recordId)
		{
			return _db.Comments.FindAll(recordId);
		}

		public async Task Delete(int commentId)
		{
			await _validator.IsCommentAuthor(commentId);
			await _db.Comments.Delete(commentId);
			await _db.SaveChanges();
		}
		public async Task Edit(EditComment comment)
		{
			await _validator.IsCommentAuthor(comment.Id);
			await _db.Comments.Edit(comment);
			await _db.SaveChanges();
		}
	}
}
