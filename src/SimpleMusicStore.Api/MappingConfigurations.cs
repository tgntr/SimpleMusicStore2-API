﻿using AutoMapper;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.MusicLibraries;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            CreateMap<NewRecord, Record>()
                .ForMember(r=>r.Artist, opt=>opt.Ignore())
                .ForMember(r=>r.Label, opt=>opt.Ignore());
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
            CreateMap<ArtistFollow, ArtistFollowDetails>()
                .ForMember(ad => ad.Name, src => src.MapFrom(af => af.Artist.Name))
                .ForMember(ad => ad.Id, src => src.MapFrom(af => af.Artist.Id));
            CreateMap<LabelFollow, LabelFollowDetails>()
                .ForMember(ld => ld.Name, src => src.MapFrom(lf => lf.Label.Name))
                .ForMember(ld => ld.Id, src => src.MapFrom(lf => lf.Label.Id));
            CreateMap<Wish, WishDetails>()
                .ForMember(rd => rd.Id, src => src.MapFrom(w => w.Record.Id))
                .ForMember(rd => rd.Title, src => src.MapFrom(w => w.Record.Title))
                .ForMember(rd => rd.Image, src => src.MapFrom(w => w.Record.Image))
                .ForMember(rd => rd.Label, src => src.MapFrom(w => w.Record.Label))
                .ForMember(rd => rd.Artist, src => src.MapFrom(w => w.Record.Artist));
            CreateMap<RecordView, ItemDetails>();
            CreateMap<Video, VideoDetails>();
            CreateMap<Track, TrackDetails>();
            CreateMap<Artist, ArtistView>();
            CreateMap<Label, LabelView>();
            CreateMap<NewOrder, Order>();
            CreateMap<NewAddress, Address>();
            CreateMap<AddressEdit, Address>();
            CreateMap<ClaimsPrincipal, User>()
                .ForMember(u => u.Id, src => src.MapFrom(c => c.FindFirstValue(ClaimTypes.NameIdentifier)))
                .ForMember(u => u.FirstName, src => src.MapFrom(c => c.FindFirstValue(ClaimTypes.GivenName)))
                .ForMember(u => u.LastName, src => src.MapFrom(c => c.FindFirstValue(ClaimTypes.Surname)))
                .ForMember(u => u.Email, src => src.MapFrom(c => c.FindFirstValue(ClaimTypes.Email)));
            CreateMap<User, UserDetails>();
            CreateMap<User, SubscriberDetails>()
                .ForMember(s => s.FollowedArtists, src => src.MapFrom(u => u.FollowedArtists.Select(fa => fa.ArtistId)))
                .ForMember(s => s.FollowedLabels, src => src.MapFrom(u => u.FollowedLabels.Select(fa => fa.LabelId)));
        }
    }
}
