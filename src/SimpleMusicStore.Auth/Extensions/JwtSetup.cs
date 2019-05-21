using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;

namespace SimpleMusicStore.Auth.Extensions
{
    public static class JwtSetup
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfigurationSection config)
        {
            services.Configure<JwtConfiguration>(config);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    //TODO should i move the lambda functions in the configurations extension?
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = config.JwtConfiguration().ValidationParameters();
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim("username"));
            });
        }

        private static JwtConfiguration JwtConfiguration(this IConfigurationSection config)
            => config.Get<JwtConfiguration>();
    }
}
