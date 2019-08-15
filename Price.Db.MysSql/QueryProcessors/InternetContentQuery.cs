using System.Data.Entity;
using Db.Entities;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;

namespace Price.Db.Postgress.QueryProcessors
{
    public class InternetContentQuery : TypedQuery<InternetContentEntity, int>, IInternetContentQuery
    {
        public InternetContentQuery(PriceContext db) : base(db)
        {
        }

        //public override IQueryable<InternetContentEntity> GetEntities()
        //{
        //    return Entities
        //        .Select(z => new InternetContentEntity
        //        {
        //            Id = z.Id,
        //            spgz_Id = z.spgz_Id,
        //            dt = z.dt,
        //            price = z.price,
        //            url = z.url,
        //            src_id = z.src_id,
        //            contact_url = z.contact_url,
        //            task_id = z.task_id,
        //            session_id = z.session_id,
        //            preview = z.preview,
        //            selected = z.selected,
        //            screenshot = null,
        //            currency = z.currency,
        //            opt = z.opt,
        //            referer = z.referer,
        //            prices = z.prices,
        //            unit_price = z.unit_price,
        //            unit = z.unit,
        //            weight = z.weight,
        //            rate = z.rate,
        //            PriceStatus = z.PriceStatus
        //        }
        //        )
        //        .AsNoTracking()
        //        .AsQueryable();
        //}
    }
}