namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class CpuWorker : BackgroundService
    {
        private readonly ICpuService cpuService;
        private readonly ILogger logger;

        public CpuWorker(
            ICpuService cpuService,
            ILogger logger)
        {
            this.cpuService = cpuService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.Info("CpuWorker started");
                await cpuService.SaveAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running CpuWorker");
            }
            finally
            {
                logger.Info("CpuWorker ended");
            }
        }
    }
}
