namespace PiControlPanel.Application.BackgroundServices
{
    using Microsoft.Extensions.Configuration;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware;

    public class OsWorker : BaseWorker<Os>
    {
        public OsWorker(
            IOsService osService,
            IConfiguration configuration,
            ILogger logger)
            : base(osService, configuration, logger)
        {
        }
    }
}
