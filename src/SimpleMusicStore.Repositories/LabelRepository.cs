using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using SimpleMusicStore.Sorting;
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
            var label = await _set.FindAsync(id);
            ValidateThatLabelExists(label);
            return _mapper.Map<LabelView>(label);
        }

        public IEnumerable<LabelDetails> FindAll(string searchTerm)
        {
            return ((IEnumerable<Label>)_set)
                .Search(searchTerm)
                .Select(_mapper.Map<LabelDetails>);
        }

        private static void ValidateThatLabelExists(Label label)
        {
            if (label == null)
                throw new ArgumentException(ErrorMessages.INVALID_LABEL);
        }
    }
}
