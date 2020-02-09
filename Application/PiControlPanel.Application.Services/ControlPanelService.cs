﻿namespace PiControlPanel.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;

    public class ControlPanelService : IControlPanelService
    {
        private readonly Infra.IControlPanelService onDemandService;
        private readonly ILogger logger;

        public ControlPanelService(Infra.IControlPanelService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Cpu> GetCpuAsync(BusinessContext context)
        {
            logger.Info("Application layer -> GetCpuAsync");
            return onDemandService.GetCpuAsync(context);
        }

        public IObservable<Cpu> GetCpuObservable(BusinessContext context)
        {
            logger.Info("Application layer -> GetCpuObservable");
            return onDemandService.GetCpuObservable(context);
        }

        public Task<bool> ShutdownAsync(BusinessContext context)
        {
            logger.Info("Application layer -> ShutdownAsync");
            return onDemandService.ShutdownAsync(context);
        }
    }
}