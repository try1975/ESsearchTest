using System.Data.Entity;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;

namespace Price.Db.MysSql.QueryProcessors
{
    public class InternetContentQuery : TypedQuery<InternetContentEntity, int>, IInternetContentQuery
    {
        public InternetContentQuery(DbContext db) : base(db)
        {
        }
    }
}