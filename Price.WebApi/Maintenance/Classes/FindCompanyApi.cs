using AutoMapper;
using Common.Dto.Model.FindCompany;
using FindCompany.Db.Entities.Entities;
using FindCompany.Db.Entities.QueryProcessors;
using Price.WebApi.Maintenance.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Price.WebApi.Maintenance.Classes
{
    class FindCompanyApi : TypedApi<FindCompanyDto, FindCompanyEntity, int>, IFindCompanyApi
    {
        public FindCompanyApi(IFindCompanyQuery query) : base(query)
        {
        }

        public int GetSellerCount(string search = null)
        {
            var queryable = Query.GetEntities();
            if (!string.IsNullOrWhiteSpace(search))
            {
                queryable = Query.GetEntities().Where(m => m.Name.Contains(search) || m.Inn.Contains(search) || m.Host.Contains(search));
            }
            return queryable.Count();
        }

        public IEnumerable<FindCompanyDto> GetLimitedFindCompany(string search = null, int pageSize = 0, int pageNum = 0)
        {
            var queryable = Query.GetEntities();
            if (!string.IsNullOrWhiteSpace(search))
            {
                queryable = Query.GetEntities().Where(m => m.Name.Contains(search) || m.Inn.Contains(search) || m.Host.Contains(search));
            }
            pageSize = pageSize > 0 ? pageSize : 100;
            pageNum = pageNum > 1 ? (pageNum - 1) * pageSize : 0;
            var list = queryable.OrderBy(i => i.Id)
                    .Skip(pageNum)
                    .Take(pageSize)
                    .ToList()
                ;
            return Mapper.Map<List<FindCompanyDto>>(list);
        }
    }
}