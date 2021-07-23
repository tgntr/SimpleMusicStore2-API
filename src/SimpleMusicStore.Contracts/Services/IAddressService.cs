﻿using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IAddressService
    {
        Task Add(NewAddress address);
        Task Edit(AddressEdit address);
        Task Remove(int id);
        IEnumerable<AddressDetails> FindAll();
    }
}
