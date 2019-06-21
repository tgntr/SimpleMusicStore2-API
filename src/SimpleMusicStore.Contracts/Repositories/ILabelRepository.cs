using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ILabelRepository : IRepository<Label>
    {
        Task<bool> Exists(int id);
        Task<LabelView> Find(int id);
        IEnumerable<LabelDetails> FindAll(string searchTerm);
    }
}
