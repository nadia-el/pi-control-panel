namespace PiControlPanel.Infrastructure.Persistence.MapperProfile
{
    using AutoMapper;
    using Models = PiControlPanel.Domain.Models.Hardware;

    public class AutoMapperConfiguration
    {
        public MapperConfiguration GetAutoMapperProfiles()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Models.Chipset, Entities.Chipset>().ReverseMap();
                    cfg.CreateMap<Models.Cpu.Cpu, Entities.Cpu.Cpu>().ReverseMap();
                    cfg.CreateMap<Models.Cpu.CpuTemperature, Entities.Cpu.CpuTemperature>()
                        .ForMember(x => x.ID, opt => opt.Ignore()).ReverseMap();
                    cfg.CreateMap<Models.Cpu.CpuAverageLoad, Entities.Cpu.CpuAverageLoad>()
                        .ForMember(x => x.ID, opt => opt.Ignore()).ReverseMap();
                    cfg.CreateMap<Models.Cpu.CpuRealTimeLoad, Entities.Cpu.CpuRealTimeLoad>()
                        .ForMember(x => x.ID, opt => opt.Ignore()).ReverseMap();
                }
            );
            return config;

        }

        public IMapper GetIMapper()
        {
            IMapper mapper = new Mapper(this.GetAutoMapperProfiles());
            return mapper;
        }
    }
}
