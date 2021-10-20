using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Api.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _comments;
        public CommentController(ICommentService comments)
        {
            _comments = comments;
        }

        [HttpGet]
        public IEnumerable<CommentView> FindAll(int recordId, int page)
        {
            return _comments.FindAll(recordId, page);
        }

        [Authorize]
        [HttpPost]
        public Task Add([FromBody] NewComment comment)
        {
            return _comments.Add(comment);
        }

        [Authorize]
        [HttpPost]
        public Task Edit([FromBody] EditComment comment)
        {
            return _comments.Edit(comment);
        }

        [Authorize]
        [HttpDelete]
        public Task Delete(int Id)
        {
            return _comments.Delete(Id);
        }
    }
}