using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.MusicLibraries;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ILabelRepository
    {
        Task Add(LabelInfo label);
        Task<bool> Exists(int id);
        Task<LabelView> Find(int id);
        IEnumerable<LabelDetails> FindAll(string searchTerm);
    }
}
