﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Auth;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Sorting;
using SimpleMusicStore.MusicLibrary;
using SimpleMusicStore.Repositories;
using SimpleMusicStore.Services;
using SimpleMusicStore.ShoppingCart;
using SimpleMusicStore.Sorting;
using SimpleMusicStore.Storage;
using SimpleMusicStore.ServiceValidations;
using SimpleMusicStore.Contracts.Validators;

namespace SimpleMusicStore.Api.Extensions
{
    public static class DependencyInjections
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticationHandler, Jwt>();
            services.AddScoped<MusicSource, Discogs>(provider => new Discogs(configuration.GetSection("Discogs")));
            services.AddScoped<FileStorage, GoogleCloud>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ILabelFollowRepository, LabelFollowRepository>();
            services.AddScoped<IArtistFollowRepository, ArtistFollowRepository>();
            services.AddScoped<IWishRepository, WishRepository>();
            services.AddScoped<IClaimHandler, ClaimHandler>();
            services.AddScoped<IClaimAccessor, ClaimAccessor>();
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IServiceValidator, ServiceValidator>();
            services.AddScoped<IShoppingCart, ShoppingCartCacheProxy>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICurrentUserActivities, CurrentUserActivities>();
            services.AddScoped<Sorter, RecordSorter>();
            services.AddScoped<IBrowseService, BrowseService>();
            services.AddAutoMapper();
        }
    }
}
