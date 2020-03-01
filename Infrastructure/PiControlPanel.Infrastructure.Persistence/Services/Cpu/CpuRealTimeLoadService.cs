namespace PiControlPanel.Infrastructure.Persistence.Services.Cpu
{
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class CpuRealTimeLoadService :
        BaseTimedService<CpuRealTimeLoad, Entities.Cpu.CpuRealTimeLoad>,
        ICpuRealTimeLoadService
    {
        public CpuRealTimeLoadService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
            : base(unitOfWork, mapper, logger)
        {
            this.repository = unitOfWork.CpuRealTimeLoadRepository;
        }
    }
}
