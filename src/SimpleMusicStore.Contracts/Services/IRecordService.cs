using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IRecordService
    {
        Task Add(NewRecord record);
        Task<bool> Exists(int id);
        Task<int> Availability(int id);
        Task<Record> Find(int id);
    }
}
