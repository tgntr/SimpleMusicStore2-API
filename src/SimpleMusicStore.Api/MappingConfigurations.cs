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
            CreateMap<Address, AddressDetails>();
            CreateMap<Record, RecordDetails>();
            CreateMap<Record, RecordView>();
            CreateMap<Label, LabelDetails>();
            CreateMap<Artist, ArtistDetails>();
            CreateMap<Order, OrderDetails>();
            CreateMap<Item, ItemDetails>()
                .ForMember(id => id.Id, src => src.MapFrom(i => i.Record.Id))
                .ForMember(id => id.Title, src => src.MapFrom(i => i.Record.Title))
                .ForMember(id => id.Image, src => src.MapFrom(i => i.Record.Image))
                .ForMember(id => id.Label, src => src.MapFrom(i => i.Record.Label))
                .ForMember(id => id.Artist, src => src.MapFrom(i => i.Record.Artist))
                .ForMember(id => id.Price, src => src.MapFrom(i => i.Record.Price))
                .ForMember(id => id.Quantity, src => src.MapFrom(i => i.Quantity));
            CreateMap<Order, OrderView>();
        }
    }
}
