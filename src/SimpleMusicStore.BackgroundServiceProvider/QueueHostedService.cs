using Microsoft.Extensions.Hosting;
using SimpleMusicStore.Contracts.BackgroundServiceProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMusicStore.BackgroundServiceProvider
{
    public class QueuedHostedService : BackgroundService
    {
        public QueuedHostedService(IBackgroundTaskQueue taskQueue)
        {
            TaskQueue = taskQueue;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected async override Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);
                await workItem(cancellationToken);
            }
        }
    }
}
