using System.IO;
using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Maintenance.Interfaces;
using PriceCommon.Enums;

namespace Price.WebApi.Maintenance.Classes
{
    public class ContentApi : TypedApi<ContentExtDto, ContentEntity, int>, IContentApi
    {
        public ContentApi(IContentQuery query) : base(query)
        {
        }

        public override bool RemoveItem(int id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return false;
            if (string.IsNullOrEmpty(entity.Screenshot)) return Query.DeleteEntity(id);
            var path = Path.Combine(AppGlobal.ScreenshotPath, entity.Screenshot);
            if (File.Exists(path)) File.Delete(path);
            return Query.DeleteEntity(id);
        }

        public bool ContentChecked(int id)
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