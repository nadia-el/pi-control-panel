namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class CpuWorker : BackgroundService
    {
        private readonly ICpuService cpuService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public CpuWorker(
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
                logger.Info("CpuWorker started");
                await cpuService.SaveAsync();

                var workerInterval = int.Parse(configuration["Worker:Interval"]);
                logger.Info($"CpuWorker configured to run at interval of {workerInterval} ms");

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Trace($"CpuWorker running at: {DateTimeOffset.Now}");
                    await this.cpuService.SaveTemperatureAsync();
                    await Task.Delay(workerInterval, stoppingToken);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running ControlPanelWorker");
            }
            finally
            {
                logger.Info("CpuWorker ended");
            }
        }
    }
}
