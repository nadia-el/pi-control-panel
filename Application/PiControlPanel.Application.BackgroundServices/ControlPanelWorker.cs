namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;

    public class ControlPanelWorker : BackgroundService
    {
        private readonly OnDemand.ICpuService cpuService;
        private readonly IChipsetService chipsetService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public ControlPanelWorker(
            OnDemand.ICpuService cpuService,
            IChipsetService chipsetService,
            IConfiguration configuration,
            ILogger logger)
        {
            this.cpuService = cpuService;
            this.chipsetService = chipsetService;
            this.configuration = configuration;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await this.chipsetService.SaveAsync();

                var workerInterval = int.Parse(configuration["Worker:Interval"]);
                logger.Info($"Worker configured to run at interval of {workerInterval} ms");

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Trace($"Worker running at: {DateTimeOffset.Now}");

                    var temperature = await this.cpuService.GetTemperatureAsync();
                    this.cpuService.PublishStatus(temperature);

                    await Task.Delay(workerInterval, stoppingToken);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running ControlPanelWorker");
            }
        }
    }
}
