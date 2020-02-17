namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;

    public class HardwareWorker : BackgroundService
    {
        private readonly ICpuService cpuService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public HardwareWorker(ICpuService cpuService, IConfiguration configuration,
            ILogger logger)
        {
            this.cpuService = cpuService;
            this.configuration = configuration;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var workerInterval = int.Parse(configuration["Worker:Interval"]);
            logger.Info($"Worker configured to run at interval of {workerInterval} ms");

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.Trace($"Worker running at: {DateTimeOffset.Now}");
                this.cpuService.PublishStatus();
                await Task.Delay(workerInterval, stoppingToken);
            }
        }
    }
}
