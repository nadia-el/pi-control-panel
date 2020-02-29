namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class MemoryWorker : BackgroundService
    {
        private readonly IMemoryService memoryService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public MemoryWorker(
            IMemoryService memoryService,
            IConfiguration configuration,
            ILogger logger)
        {
            this.memoryService = memoryService;
            this.configuration = configuration;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.Info("MemoryWorker started");
                await this.memoryService.SaveAsync();

                var workerInterval = int.Parse(configuration["Worker:Interval"]);
                logger.Info($"MemoryWorker configured to run at interval of {workerInterval} ms");

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Trace($"MemoryWorker running at: {DateTimeOffset.Now}");
                    await this.memoryService.SaveStatusAsync();
                    await Task.Delay(workerInterval, stoppingToken);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running MemoryWorker");
            }
            finally
            {
                logger.Info("MemoryWorker ended");
            }
        }
    }
}
