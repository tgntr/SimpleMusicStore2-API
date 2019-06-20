using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class LabelRepository : DbRepository<Label>, ILabelRepository
    {
        public LabelRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            :base(db, mapper)
        {
        }

        public Task<bool> Exists(int id)
        {
            return _set.AnyAsync(l => l.Id == id);
        }

        public async Task<LabelView> Find(int id)
        {
            return _mapper.Map<LabelView>(await _set.FindAsync(id));
        }
    }
}
