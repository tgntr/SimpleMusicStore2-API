using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Data
{
    public static class DbSetup
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SimpleMusicStoreDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString));
        }
    }
}
