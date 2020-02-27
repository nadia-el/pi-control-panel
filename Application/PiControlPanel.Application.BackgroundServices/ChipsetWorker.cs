namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class ChipsetWorker : BackgroundService
    {
        private readonly IChipsetService chipsetService;
        private readonly ILogger logger;

        public ChipsetWorker(
            IChipsetService chipsetService,
            ILogger logger)
        {
            this.chipsetService = chipsetService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.Info("ChipsetWorker started");
                await this.chipsetService.SaveAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running ChipsetWorker");
            }
            finally
            {
                logger.Info("ChipsetWorker ended");
            }
        }
    }
}
