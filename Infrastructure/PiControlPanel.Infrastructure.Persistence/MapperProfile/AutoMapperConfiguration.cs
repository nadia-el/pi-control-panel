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
