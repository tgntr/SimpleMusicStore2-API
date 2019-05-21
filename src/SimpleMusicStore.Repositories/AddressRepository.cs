using AutoMapper;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class AddressRepository : ListRepository<Address>, IAddressRepository
    {
        public AddressRepository(IMapper mapper)
            : base(mapper)
        {
        }

        public Task<bool> Exists(int id)
        {
            return Task.Run(()=>_set.Any(a => a.Id == id));
        }

        public Task<bool> Exists(int id, string userId)
        {
            return Task.Run(() => _set.Any(a => a.Id == id && a.UserId == userId));
        }

        public Task<IEnumerable<AddressDto>> FindAll(string userId)
        {
            //TODO is it an okay way to map stuff? Is it good to map things here?
            return Task.Run(() => _set.Where(a => a.UserId == userId).Select(_mapper.Map<AddressDto>));
        }
    }
}
