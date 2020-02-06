namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Infrastructure.OnDemand.Utils;
    
    public class ControlPanelService : IControlPanelService
    {
        private readonly ILogger logger;

        public ControlPanelService(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<Hardware> GetHardwareAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> GetHardwareAsync");
            return Task.FromResult(new Hardware()
            {
                Cpu = new Cpu()
                {
                    Temperature = GetTemperature()
                }
            });
        }

        private double GetTemperature()
        {
            var result = Constants.TemperatureCommand.Bash();
            logger.Debug($"GetTemperature from command: '{result}'");
            var temperatureResult = result.Substring(result.IndexOf('=') + 1, result.IndexOf("'") - (result.IndexOf('=') + 1));
            logger.Debug($"Temperature substring: '{temperatureResult}'");
            double temperature;
            if (double.TryParse(temperatureResult, out temperature)) {
                return temperature;
            }
            logger.Warn($"Could not parse temperature: '{temperatureResult}'");
            return 0.0;
        }
    }
}
