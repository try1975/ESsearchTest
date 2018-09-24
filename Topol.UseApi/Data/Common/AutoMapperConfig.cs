using AutoMapper.Configuration;
using Common.Dto.Model.FindCompany;
using Topol.UseApi.Interfaces;

namespace Topol.UseApi.Data.Common
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings(MapperConfigurationExpression cfg)
        {

            cfg.CreateMap<FindCompanyDto, FindCompanyDto>()
                ;
            cfg.CreateMap<FindCompanyDto, ISellerView>()
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Name))
                ;
            cfg.CreateMap<ISellerView, FindCompanyDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SellerName))
                ;
        }
    }
}