using Db.Entities;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;

namespace Price.Db.Postgress.QueryProcessors
{
    public class ScheduleQuery : TypedQuery<ScheduleEntity, int>, IScheduleQuery
    {
        public ScheduleQuery(PriceContext db) : base(db)
        {
        }
    }
}
