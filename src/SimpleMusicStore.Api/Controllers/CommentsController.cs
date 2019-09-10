using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;

namespace SimpleMusicStore.Api.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService _comments;
        public CommentsController(ICommentsService comments)
        {
            _comments = comments;
        }

        [HttpGet]
        public IEnumerable<Comment> All(int id)
        {
            return _comments.All(id);
        }

        //[Authorize]
        [HttpPost]
        public Task Add([FromBody] NewComment comment)
        {
            return _comments.Add(comment);
        }

        //[Authorize]
        [HttpPost]
        public Task Edit([FromBody]EditComment comment)
        {
            return _comments.Edit(comment);
        }

        //[Authorize]
        [HttpDelete]
        public Task Delete(int commentId)
        {
            return _comments.Delete(commentId);
        }
    }
}