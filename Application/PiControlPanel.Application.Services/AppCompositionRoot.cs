namespace PiControlPanel.Application.Services
{
    using LightInject;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System.Reactive.Subjects;

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
            serviceRegistry.RegisterScoped<IControlPanelService, Infrastructure.OnDemand.Services.ControlPanelService>();
            serviceRegistry.RegisterScoped<IChipsetService, Infrastructure.OnDemand.Services.ChipsetService>();
            serviceRegistry.RegisterScoped<ICpuService, Infrastructure.OnDemand.Services.CpuService>();
            serviceRegistry.RegisterScoped<IMemoryService, Infrastructure.OnDemand.Services.MemoryService>();
            serviceRegistry.RegisterScoped<IUserAccountService, Infrastructure.Persistence.Services.UserAccountService>();
            serviceRegistry.RegisterSingleton<ISubject<Cpu>>(factory => new ReplaySubject<Cpu>(1));
        }
    }
}
