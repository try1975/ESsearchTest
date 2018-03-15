using System.IO;
using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Maintenance.Interfaces;
using PriceCommon.Enums;

namespace Price.WebApi.Maintenance.Classes
{
    public class InternetContentApi : TypedApi<InternetContentDto, InternetContentEntity, int>, IInternetContentApi
    {
        public InternetContentApi(IInternetContentQuery query) : base(query)
        {
        }

        public override bool RemoveItem(int id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return false;
            if (string.IsNullOrEmpty(entity.contact_url)) return Query.DeleteEntity(id);
            var path = Path.Combine(AppGlobal.ScreenshotPath, entity.contact_url);
            if (File.Exists(path)) File.Delete(path);
            return Query.DeleteEntity(id);
        }

        public bool InternetContentChecked(int id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return false;
            if (entity.PriceStatus == PriceStatus.Checked) return false;
            entity.PriceStatus = PriceStatus.Checked;
            Query.UpdateEntity(entity);
            return true;
        }
    }
}