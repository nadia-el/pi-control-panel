namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    
    public abstract class BaseService<T> : IBaseService<T>
    {
        protected readonly ILogger logger;

        public BaseService(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<T> GetAsync()
        {
            logger.Info($"Infra layer -> BaseService<{nameof(T)}> -> GetAsync");
            var model = this.GetModel();
            return Task.FromResult(model);
        }

        protected abstract T GetModel();
    }
}
