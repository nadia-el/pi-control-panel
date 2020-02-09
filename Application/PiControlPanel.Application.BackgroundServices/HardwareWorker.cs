namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;

    public class HardwareWorker : BackgroundService
    {
        private readonly IControlPanelService controlPanelService;
        private readonly ILogger logger;

        public HardwareWorker(IControlPanelService controlPanelService, ILogger logger)
        {
            this.controlPanelService = controlPanelService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.Info($"Worker running at: {DateTimeOffset.Now}");
                this.controlPanelService.PublishCpu();
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
