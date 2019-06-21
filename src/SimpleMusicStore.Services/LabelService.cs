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
        private readonly ILabelRepository _labels;
        private readonly IMapper _mapper;
        private readonly MusicSource _discogs;
        private readonly IServiceValidator _validator;
        private readonly ICurrentUserActivities _currentUser;

        public LabelService(ILabelRepository labels,
            IMapper mapper,
            MusicSource discogs,
            IServiceValidator validator,
            ICurrentUserActivities currentUser)
        {
            _labels = labels;
            _mapper = mapper;
            _discogs = discogs;
            _validator = validator;
            _currentUser = currentUser;
        }

        public async Task Add(int discogsId)
        {
            if (!await _labels.Exists(discogsId))
                await AddLabel(discogsId);
        }

        public async Task<LabelView> Find(int id)
        {
            return await GenerateLabelView(id);
        }

        private async Task<LabelView> GenerateLabelView(int id)
        {
            var label = await _labels.Find(id);
            label.IsFollowed = _currentUser.IsLabelFollowed(id);
            label.Records = label.Records.OrderByDescending(r => r.DateAdded);
            return label;
        }

        private async Task AddLabel(int discogsId)
        {
            var labelInfo = await _discogs.Label(discogsId);
            var label = _mapper.Map<Label>(labelInfo);
            await _labels.Add(label);
        }
    }
}
