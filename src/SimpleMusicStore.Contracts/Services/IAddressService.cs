using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IAddressService
    {
        Task Add(AddressDetails newAddress);
        Task Edit(AddressDetails newAddress);
        Task Remove(int id);
    }
}
