namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class OsWorker : BackgroundService
    {
        private readonly IOsService osService;
        private readonly ILogger logger;

        public OsWorker(
            IOsService osService,
            ILogger logger)
        {
            this.osService = osService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.Info("OsWorker started");
                await osService.SaveAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running OsWorker");
            }
            finally
            {
                logger.Info("OsWorker ended");
            }
        }
    }
}
