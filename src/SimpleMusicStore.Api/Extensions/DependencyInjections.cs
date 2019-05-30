
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Auth;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.MusicLibrary;
using SimpleMusicStore.Repositories;
using SimpleMusicStore.Services;
using SimpleMusicStore.Storage;

namespace SimpleMusicStore.Api.Extensions
{
    public static class DependencyInjections
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticationHandler, Jwt>();
            services.AddScoped<MusicSource, Discogs>(provider => new Discogs(configuration.GetSection("Discogs")));
            services.AddScoped<FileStorage, GoogleCloud>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ShoppingCart, Redis>();
            services.AddScoped<ILabelFollowRepository, LabelFollowRepository>();
            services.AddScoped<IArtistFollowRepository, ArtistFollowRepository>();
            services.AddScoped<IWishRepository, WishRepository>();
            services.AddScoped<IServiceValidations, ServiceValidations>();
            services.AddAutoMapper();
        }
    }
}
