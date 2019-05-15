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
    }
}
