using AutoMapper;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using System;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _labels;
        private readonly IMapper _mapper;
        private readonly MusicSource _discogs;
        private readonly IServiceValidations _validator;

        public LabelService(
            ILabelRepository labels, 
            IMapper mapper, 
            MusicSource discogs,
            IServiceValidations validator)
        {
            _labels = labels;
            _mapper = mapper;
            _discogs = discogs;
            _validator = validator;
        }

        public async Task Add(int discogsId)
        {
            if (!await _labels.Exists(discogsId))
                await AddLabel(discogsId);
        }

        private async Task AddLabel(int discogsId)
        {
            var labelInfo = await _discogs.Label(discogsId);
            var label = _mapper.Map<Label>(labelInfo);
            await _labels.Add(label);
        }

        
    }
}
