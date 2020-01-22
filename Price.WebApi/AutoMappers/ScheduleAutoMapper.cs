using AutoMapper.Configuration;
using Common.Dto.Model;
using Price.Db.Entities.Entities;


namespace Price.WebApi.AutoMappers
{
    public class ScheduleAutoMapper
    {

        public static void Configure(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ScheduleDto, ScheduleEntity>()
                ;
            cfg.CreateMap<ScheduleEntity, ScheduleDto>()
                ;
        }
    }
}