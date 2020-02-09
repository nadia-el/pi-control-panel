namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Common;
    
    public class ControlPanelService : IControlPanelService
    {
        private readonly ISubject<Cpu> cpuSubject;
        private readonly ILogger logger;

        public ControlPanelService(ISubject<Cpu> cpuSubject, ILogger logger)
        {
            this.cpuSubject = cpuSubject;
            this.logger = logger;
        }

        public Task<Cpu> GetCpuAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> GetCpuAsync");
            var cpu = this.GetCpu();
            return Task.FromResult(cpu);
        }

        public void PublishCpu()
        {
            logger.Info("Infra layer -> PublishCpu");
            var cpu = this.GetCpu();
            this.cpuSubject.OnNext(cpu);
        }

        public IObservable<Cpu> GetCpuObservable(BusinessContext context)
        {
            return this.cpuSubject.AsObservable();
        }

        public Task<bool> ShutdownAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> ShutdownAsync");
            var result = Constants.ShutdownCommand.Bash();
            logger.Debug($"Result of ShutdownAsync from command: '{result}'");
            return Task.FromResult(true);
        }

        private Cpu GetCpu()
        {
            return new Cpu()
            {
                Temperature = GetTemperature(),
                
            };
        }

        private double GetTemperature()
        {
            var result = Constants.MeasureTempCommand.Bash();
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
