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
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressService _addresses;

        public AddressController(IAddressService addresses)
            : base()
        {
            _addresses = addresses;
        }

        [HttpPost]
        public async Task Add([FromBody]NewAddress address)
        {
            await _addresses.Add(address);
        }

        [HttpPost]
        public async Task Edit([FromBody]AddressEdit address)
        {
            await _addresses.Edit(address);
        }

        [HttpPost]
        public async Task Remove(int id)
        {
            await _addresses.Remove(id);
        }

        public IEnumerable<AddressDetails> FindAll(string userId)
        {
            return _addresses.FindAll(userId);
        }
    }
}