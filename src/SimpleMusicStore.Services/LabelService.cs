using AutoMapper;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class LabelService : ILabelService
    {
        private readonly IUnitOfWork _db;

        public LabelService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<LabelView> Find(int id)
        {
            return await GenerateLabelView(id);
        }

        private async Task<LabelView> GenerateLabelView(int id)
        {
            var label = await _db.Labels.Find(id);
            label.IsFollowed = _db.CurrentUser.IsLabelFollowed(id);
            return label;
        }
    }
}
