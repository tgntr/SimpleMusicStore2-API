using AutoMapper;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _labels;
        private readonly IMapper _mapper;
        private readonly MusicSource _discogs;

        public LabelService(ILabelRepository labels, IMapper mapper, MusicSource discogs)
        {
            _labels = labels;
            _mapper = mapper;
            _discogs = discogs;
        }

        public async Task Add(int discogsId)
        {
            if (await _labels.Exists(discogsId))
                return;

            var labelInfo = _discogs.Label(discogsId);
            var label = _mapper.Map<Label>(labelInfo);
            await _labels.Add(label);
        }
    }
}
