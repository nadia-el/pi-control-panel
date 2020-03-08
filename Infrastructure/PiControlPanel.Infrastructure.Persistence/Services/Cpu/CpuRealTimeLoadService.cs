namespace PiControlPanel.Infrastructure.Persistence.Services.Cpu
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CpuRealTimeLoadService :
        BaseTimedService<CpuRealTimeLoad, Entities.Cpu.CpuRealTimeLoad>,
        ICpuRealTimeLoadService
    {
        public CpuRealTimeLoadService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
            : base(unitOfWork, mapper, logger)
        {
            this.repository = unitOfWork.CpuRealTimeLoadRepository;
        }

        public async Task<IDictionary<DateTime, CpuRealTimeLoad>> GetRealTimeLoadsAsync(
            IEnumerable<DateTime> dateTimes)
        {
            var entities = await this.repository.GetMany(l => dateTimes.Contains(l.DateTime))
                .ToDictionaryAsync(i => i.DateTime, i => i);
            return mapper.Map<Dictionary<DateTime, CpuRealTimeLoad>>(entities);
        }
    }
}
