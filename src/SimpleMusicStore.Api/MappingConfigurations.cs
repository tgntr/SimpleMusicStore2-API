using AutoMapper;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.MusicLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Api
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations()
        {
            CreateMap<LabelInfo, Label>();
            CreateMap<ArtistInfo, Artist>();
            CreateMap<RecordVideoInfo, Video>();
            CreateMap<RecordTrackInfo, Track>();
            CreateMap<RecordInfo, Record>();
        }
    }
}
