using AutoMapper.Configuration;
using Common.Dto.Model.FindCompany;
using FindCompany.Db.Entities.Entities;

namespace Price.WebApi.AutoMappers
{
    public class FindCompanyAutoMapper
    {
        public static void Configure(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<FindCompanyEntity, FindCompanyDto>()
                ;
            cfg.CreateMap<FindCompanyDto, FindCompanyEntity>()
                ;
        }
    }
}