namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class DiskWorker : BackgroundService
    {
        private readonly IDiskService diskService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public DiskWorker(
            IDiskService diskService,
            IConfiguration configuration,
            ILogger logger)
        {
            this.diskService = diskService;
            this.configuration = configuration;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.Info("DiskWorker started");
                await this.diskService.SaveAsync();

                var workerInterval = int.Parse(configuration["Worker:Interval"]);
                logger.Info($"DiskWorker configured to run at interval of {workerInterval} ms");

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Trace($"DiskWorker running at: {DateTimeOffset.Now}");
                    await this.diskService.SaveStatusAsync();
                    await Task.Delay(workerInterval, stoppingToken);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running DiskWorker");
            }
            finally
            {
                logger.Info("DiskWorker ended");
            }
        }
    }
}
