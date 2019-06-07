using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;

namespace SimpleMusicStore.Api.Controllers
{
    public class ValuesController : Controller
    {
        private readonly UserManager<SimpleUser> _users;

        public ValuesController(IOptions<JwtConfiguration> config, UserManager<SimpleUser> users)
            : base()
		{
			var a = config;
            _users = users;
		}
        // GET api/values
        [HttpGet]
		public async Task Test()
        {
            await _users.CreateAsync(new SimpleUser { UserName = "tgntr" }, "test");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
