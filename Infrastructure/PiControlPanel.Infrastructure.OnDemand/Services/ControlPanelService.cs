namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Contracts.Util;
    using PiControlPanel.Domain.Models;
    
    public class ControlPanelService : IControlPanelService
    {
        private readonly ILogger logger;

        public ControlPanelService(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<bool> ShutdownAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> ShutdownAsync");
            var result = BashCommands.SudoShutdown.Bash();
            logger.Debug($"Result of ShutdownAsync from command: '{result}'");
            return Task.FromResult(true);
        }
    }
}
