using AutoMapper.Configuration;
using Common.Dto.Model;
using Common.Dto.Model.NewApi;
using Common.Dto.Model.Packet;
using Common.Dto.Model.XPath;
using Price.Db.Entities.Entities;
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
            cfg.CreateMap<ContentDto, XPathDto>()
                ;
            cfg.CreateMap<XPathDto, ContentDto>()
                ;

            cfg.CreateMap<PacketEntity, PacketDto>()
                ;
            cfg.CreateMap<PacketDto, PacketEntity>()
                ;
            cfg.CreateMap<SearchItemEntity, SearchItemExtDto>()
                ;
            cfg.CreateMap<SearchItemExtDto, SearchItemEntity>()
                ;

            cfg.CreateMap<SearchItemDto, SearchItemExtDto>()
                ;
            cfg.CreateMap<SearchItemExtDto, SearchItemDto>()
                ;

            cfg.CreateMap<InternetContentDto, InternetContentEntity>()
                ;
            cfg.CreateMap<InternetContentEntity, InternetContentDto>()
                ;

            cfg.CreateMap<ContentExtDto, ContentEntity>()
                ;
            cfg.CreateMap<ContentEntity, ContentExtDto>()
                ;

        }
    }
}