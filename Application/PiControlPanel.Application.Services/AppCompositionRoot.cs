namespace PiControlPanel.Application.Services
{
    using System.Reactive.Subjects;
    using LightInject;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    using PiControlPanel.Infrastructure.Persistence.Repositories;
    using AutoMapper;
    using PiControlPanel.Infrastructure.Persistence.MapperProfile;
    using Contracts = PiControlPanel.Domain.Contracts.Infrastructure;
    using Persistence = PiControlPanel.Infrastructure.Persistence.Services;
    using OnDemand = PiControlPanel.Infrastructure.OnDemand.Services;

    /// <summary>
    ///     Implementation of LightInject's ICompositionRoot responsible for
    ///     registering all services required for the Application layer
    /// </summary>
    public class AppCompositionRoot : ICompositionRoot
    {
        /// <summary>
        ///     Called after LightInject ServiceContainer RegisterFor method is called
        /// </summary>
        /// <param name="serviceRegistry">LightInject's service registry</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped<IUnitOfWork, UnitOfWork>();
            serviceRegistry.RegisterScoped<Contracts.Persistence.IChipsetService, Persistence.ChipsetService>();
            serviceRegistry.RegisterScoped<Contracts.Persistence.ICpuService, Persistence.CpuService>();

            serviceRegistry.RegisterScoped<Contracts.OnDemand.IControlPanelService, OnDemand.ControlPanelService>();
            serviceRegistry.RegisterScoped<Contracts.OnDemand.IChipsetService, OnDemand.ChipsetService>();
            serviceRegistry.RegisterScoped<Contracts.OnDemand.ICpuService, OnDemand.CpuService>();
            serviceRegistry.RegisterScoped<Contracts.OnDemand.IMemoryService, OnDemand.MemoryService>();
            serviceRegistry.RegisterScoped<Contracts.OnDemand.IGpuService, OnDemand.GpuService>();
            serviceRegistry.RegisterScoped<Contracts.OnDemand.IDiskService, OnDemand.DiskService>();
            serviceRegistry.RegisterScoped<Contracts.OnDemand.IOsService, OnDemand.OsService>();
            serviceRegistry.RegisterScoped<Contracts.OnDemand.IUserAccountService, OnDemand.UserAccountService>();
            
            serviceRegistry.RegisterSingleton<IMapper>(factory => new AutoMapperConfiguration().GetIMapper());
            serviceRegistry.RegisterSingleton<ISubject<CpuTemperature>>(factory => new ReplaySubject<CpuTemperature>(1));
        }
    }
}
