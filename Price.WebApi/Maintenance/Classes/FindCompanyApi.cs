using Common.Dto.Model.FindCompany;
using FindCompany.Db.Entities.Entities;
using FindCompany.Db.Entities.QueryProcessors;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Maintenance.Classes
{
    class FindCompanyApi : TypedApi<FindCompanyDto, FindCompanyEntity, int>, IFindCompanyApi
    {
        public FindCompanyApi(IFindCompanyQuery query) : base(query)
        {
        }
    }
}