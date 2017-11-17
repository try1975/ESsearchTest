using AutoMapper.Configuration;
using Common.Dto.Model;
using PriceCommon.Model;

namespace Price.WebApi.AutoMappers
{
    public class ContentAutoMapper
    {
        public static void Configure(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Content, ContentDto>()
                ;
            cfg.CreateMap<ContentDto, Content>()
                ;
        }
    }
}