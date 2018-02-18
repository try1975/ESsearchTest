using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Maintenance.Classes
{
    public class ContentApi : TypedApi<ContentExtDto, ContentEntity, int>, IContentApi
    {
        public ContentApi(IContentQuery query) : base(query)
        {
        }
    }
}