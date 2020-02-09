namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Infrastructure.Common;
    
    public class ControlPanelService : IControlPanelService
    {
        private readonly ISubject<Hardware> hardwareSubject;
        private readonly ILogger logger;

        public ControlPanelService(ISubject<Hardware> hardwareSubject, ILogger logger)
        {
            this.hardwareSubject = hardwareSubject;
            this.logger = logger;
        }

        public Task<Hardware> GetHardwareAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> GetHardwareAsync");
            var hardware = this.GetHardware();
            return Task.FromResult(hardware);
        }

        public void PublishHardware()
        {
            logger.Info("Infra layer -> PublishHardware");
            var hardware = this.GetHardware();
            this.hardwareSubject.OnNext(hardware);
        }

        public IObservable<Hardware> GetHardwareObservable(BusinessContext context)
        {
            return this.hardwareSubject.AsObservable();
        }

        public Task<bool> ShutdownAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> ShutdownAsync");
            var result = Constants.ShutdownCommand.Bash();
            logger.Debug($"Result of ShutdownAsync from command: '{result}'");
            return Task.FromResult(true);
        }

        private Hardware GetHardware()
        {
            return new Hardware()
            {
                Cpu = new Cpu()
                {
                    Temperature = GetTemperature()
                }
            };
        }

        private double GetTemperature()
        {
            var result = Constants.TemperatureCommand.Bash();
            logger.Debug($"Result of GetTemperature from command: '{result}'");
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
