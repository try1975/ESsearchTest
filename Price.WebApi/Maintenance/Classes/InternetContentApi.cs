using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Maintenance.Classes
{
    public class InternetContentApi : TypedApi<InternetContentDto, InternetContentEntity, int>, IInternetContentApi
    {
        public InternetContentApi(IInternetContentQuery query) : base(query)
        {
        }
    }
}