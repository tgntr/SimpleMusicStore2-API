using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;

namespace SimpleMusicStore.Api.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addresses;
        public AddressController(IAddressService addresses)
            : base()
        {
            _addresses = addresses;
        }
        public async Task Add([FromBody]AddressDetails newAddress)
        {
            //TODO should i ModelState validate??(because Required fields in the binding model)
            await _addresses.Add(newAddress);
        }

        public async Task Edit([FromBody]AddressDetails newAddress)
        {
            await _addresses.Edit(newAddress);
        }

        public async Task Remove(int id)
        {
            await _addresses.Remove(id);
        }
    }
}