namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class NetworkInterfaceStatusWorker : BackgroundService
    {
        protected readonly INetworkService networkService;
        protected readonly IConfiguration configuration;
        protected readonly ILogger logger;

        public NetworkInterfaceStatusWorker(
            INetworkService networkService,
            IConfiguration configuration,
            ILogger logger)
        {
            this.networkService = networkService;
            this.configuration = configuration;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                bool.TryParse(configuration[$"Workers:NetworkInterfaceStatus:Enabled"], out var enabled);
                if (!enabled)
                {
                    logger.Warn($"NetworkInterfaceStatusWorker is not enabled, returning...");
                    return;
                }

                logger.Info($"NetworkInterfaceStatusWorker started");

                var workerInterval = int.Parse(configuration["Workers:NetworkInterfaceStatus:Interval"]);
                if (workerInterval <= 0)
                {
                    logger.Debug($"NetworkInterfaceStatusWorker has no interval set for recurring task, returning...");
                    return;
                }

                logger.Info($"NetworkInterfaceStatusWorker configured to run at interval of {workerInterval} ms");
                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Debug($"NetworkInterfaceStatusWorker running at: {DateTimeOffset.Now}");
                    await this.SaveRecurring(workerInterval);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"error running NetworkInterfaceStatusWorker");
            }
            finally
            {
                logger.Info($"NetworkInterfaceStatusWorker ended");
            }
        }

        protected async Task SaveRecurring(int samplingInterval)
        {
            await this.networkService.SaveNetworkInterfacesStatusAsync(samplingInterval);
        }
    }
}
