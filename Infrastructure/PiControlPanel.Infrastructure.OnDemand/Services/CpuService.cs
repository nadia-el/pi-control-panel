namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Common;

    public class CpuService : ICpuService
    {
        private readonly ISubject<Cpu> cpuSubject;
        private readonly ILogger logger;

        public CpuService(ISubject<Cpu> cpuSubject, ILogger logger)
        {
            this.cpuSubject = cpuSubject;
            this.logger = logger;
        }

        public Task<Cpu> GetAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> GetAsync");
            var cpu = this.GetGpu();
            return Task.FromResult(cpu);
        }

        public Task<double> GetTemperatureAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> GetTemperatureAsync");
            var temperature = this.GetTemperature();
            return Task.FromResult(temperature);
        }

        public void PublishTemperature()
        {
            logger.Info("Infra layer -> PublishTemperature");
            var temperature = this.GetTemperature();
            this.cpuSubject.OnNext(new Cpu() { Temperature = temperature });
        }

        public IObservable<Cpu> GetCpuObservable(BusinessContext context)
        {
            logger.Info("Infra layer -> GetCpuObservable");
            return this.cpuSubject.AsObservable();
        }

        private Cpu GetGpu()
        {
            var result = Constants.CatProcCpuInfoCommand.Bash();
            logger.Debug($"Result of CatProcCpuInfo from command: '{result}'");
            string[] lines = result.Split(new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
            var cores = lines.Count(line => line.StartsWith("processor"));
            logger.Debug($"Number of cores: '{cores}'");
            var model = lines.Last(line => line.StartsWith("model name"))
                .Split(':')[1].Trim();
            logger.Debug($"Cpu model: '{model}'");
            return new Cpu()
            {
                Cores = cores,
                Model = model
            };
        }

        private double GetTemperature()
        {
            var result = Constants.MeasureTempCommand.Bash();
            logger.Debug($"Result of GetTemperature from command: '{result}'");
            var temperatureResult = result.Substring(result.IndexOf('=') + 1, result.IndexOf("'") - (result.IndexOf('=') + 1));
            logger.Debug($"Temperature substring: '{temperatureResult}'");
            double temperature;
            if (double.TryParse(temperatureResult, out temperature))
            {
                return temperature;
            }
            logger.Warn($"Could not parse temperature: '{temperatureResult}'");
            return 0.0;
        }
    }
}
