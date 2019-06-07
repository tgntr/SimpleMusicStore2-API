using AutoMapper;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.MusicLibraries;
using SimpleMusicStore.Models.View;
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
            //TODO seperate things
            CreateMap<LabelInfo, Label>();
            CreateMap<ArtistInfo, Artist>();
            CreateMap<RecordVideoInfo, Video>();
            CreateMap<RecordTrackInfo, Track>();
            CreateMap<RecordInfo, Record>();
            CreateMap<Record, CartItem>();

            CreateMap<KeyValuePair<int, int>, Item>()
                .ForMember(i => i.RecordId, src => src.MapFrom(kvp => kvp.Key))
                .ForMember(i => i.Quantity, src => src.MapFrom(kvp => kvp.Value));

            CreateMap<AddressDetails, Address>();
        }
    }
}
