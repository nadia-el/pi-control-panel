namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class GpuWorker : BackgroundService
    {
        private readonly IGpuService gpuService;
        private readonly ILogger logger;

        public GpuWorker(
            IGpuService gpuService,
            ILogger logger)
        {
            this.gpuService = gpuService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.Info("GpuWorker started");
                await gpuService.SaveAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running GpuWorker");
            }
            finally
            {
                logger.Info("GpuWorker ended");
            }
        }
    }
}
