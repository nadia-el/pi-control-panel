namespace PiControlPanel.Application.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;

    public class CpuRealTimeLoadWorker : BackgroundService
    {
        private readonly ICpuService cpuService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public CpuRealTimeLoadWorker(
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
                logger.Info("CpuRealTimeLoadWorker started");
                var workerInterval = int.Parse(configuration["Worker:Interval"]);
                logger.Info($"CpuRealTimeLoadWorker configured to run at interval of {workerInterval} ms");

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Trace($"CpuRealTimeLoadWorker running at: {DateTimeOffset.Now}");
                    await this.cpuService.SaveRealTimeLoadAsync();
                    await Task.Delay(workerInterval, stoppingToken);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "error running CpuRealTimeLoadWorker");
            }
            finally
            {
                logger.Info("CpuRealTimeLoadWorker ended");
            }
        }
    }
}
