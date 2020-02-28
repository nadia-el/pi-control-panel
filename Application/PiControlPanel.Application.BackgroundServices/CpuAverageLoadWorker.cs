namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class CpuAverageLoadWorker : BackgroundService
    {
        private readonly ICpuService cpuService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public CpuAverageLoadWorker(
            ICpuService cpuService,
            IConfiguration configuration,
            ILogger logger)
        {
            this.cpuService = cpuService;
            this.configuration = configuration;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.Info("CpuAverageLoadWorker started");
                var workerInterval = int.Parse(configuration["Worker:Interval"]);
                logger.Info($"CpuAverageLoadWorker configured to run at interval of {workerInterval} ms");

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Trace($"CpuAverageLoadWorker running at: {DateTimeOffset.Now}");
                    await this.cpuService.SaveAverageLoadAsync();
                    await Task.Delay(workerInterval, stoppingToken);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running CpuAverageLoadWorker");
            }
            finally
            {
                logger.Info("CpuAverageLoadWorker ended");
            }
        }
    }
}
