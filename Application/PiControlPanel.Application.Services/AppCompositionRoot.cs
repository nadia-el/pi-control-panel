namespace PiControlPanel.Application.Services
{
    using LightInject;
    using PiControlPanel.Domain.Contracts.Infrastructure;
    using Infra = PiControlPanel.Infrastructure.Persistence.Services;

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
            serviceRegistry.RegisterScoped<IControlPanelService, Infra.ControlPanelService>();
            serviceRegistry.RegisterScoped<IUserAccountService, Infra.UserAccountService>();
        }
    }
}
