using Ethereal.App.Services;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ethereal.App.ScheduledTasks
{
    public class RecurringTasks : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public RecurringTasks(IHostApplicationLifetime hostApplicationLifetime) => _hostApplicationLifetime = hostApplicationLifetime;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //注册任务
            _hostApplicationLifetime.ApplicationStarted.Register(async () =>
            {
                RecurringJob.AddOrUpdate<IValueService>("测试", c => c.Write(), Cron.Minutely(), TimeZoneInfo.Local);

                await Task.CompletedTask;
            });
            await Task.CompletedTask;
        }
    }
}
