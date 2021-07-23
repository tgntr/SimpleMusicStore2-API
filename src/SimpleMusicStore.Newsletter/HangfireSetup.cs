using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleMusicStore.Newsletter
{
    public static class HangfireSetup
    {
        public static void AddHangfire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(configuration =>
                configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(
                        connectionString,
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true
                        }));

            services.AddHangfireServer();

        }
    }
}
