
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IdentityHandler, UserManager>();
            services.AddScoped<MusicSource, Discogs>();
            services.AddScoped<FileStorage, GoogleCloud>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddAutoMapper();
        }
    }
}
