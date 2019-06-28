using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Contracts.BackgroundServiceProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.BackgroundServiceProvider
{
    public static class BackgroundServiceSetup
    {
        public static void AddBackgroundServiceProvider (this IServiceCollection services)
        {
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        }
    }
}
