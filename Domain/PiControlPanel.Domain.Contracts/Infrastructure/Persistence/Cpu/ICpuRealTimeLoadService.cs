namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu
{
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICpuRealTimeLoadService : IBaseTimedObjectService<CpuRealTimeLoad>
    {
        Task<IDictionary<DateTime, CpuRealTimeLoad>> GetRealTimeLoadsAsync(
            IEnumerable<DateTime> dateTimes);
    }
}
