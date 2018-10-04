using System;
using System.Globalization;
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
            try
            {
                var entity = Query.GetEntity(id);
                if (entity == null) return false;
                if (entity.PriceStatus == PriceStatus.Checked) return false;
                entity.PriceStatus = PriceStatus.Checked;
                Query.UpdateEntity(entity);
                return true;
            }
            catch (Exception exception)
            {
                Logger.Log.Error(exception);

            }
            return false;
        }

        public bool InternetContentNotChecked(int id)
        {
            try
            {
                var entity = Query.GetEntity(id);
                if (entity == null) return false;
                if (entity.PriceStatus == PriceStatus.NotChecked) return false;
                entity.PriceStatus = PriceStatus.NotChecked;
                Query.UpdateEntity(entity);
                return true;
            }
            catch (Exception exception)
            {
                Logger.Log.Error(exception);
            }
            return false;
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool InternetContentSetPrice(int id, string price)
        {
            try
            {
                if (string.IsNullOrEmpty(price)) return false;
                var entity = Query.GetEntity(id);
                if (entity == null) return false;
                entity.price = float.Parse(price, CultureInfo.InvariantCulture.NumberFormat);
                entity.ManualPrice = 1;
                entity = Query.UpdateEntity(entity);
                return entity != null;
            }
            catch (Exception exception)
            {
                Logger.Log.Error(exception);

            }
            return false;
        }

        public bool InternetContentRejected(int id, string reason)
        {
            try
            {
                var entity = Query.GetEntity(id);
                if (entity == null) return false;
                if (entity.PriceStatus == PriceStatus.Rejected && entity.RejectReason == reason) return false;
                entity.PriceStatus = PriceStatus.Checked;
                entity.RejectReason = reason;
                Query.UpdateEntity(entity);
                return true;
            }
            catch (Exception exception)
            {
                Logger.Log.Error(exception);

            }
            return false;
        }
    }
}