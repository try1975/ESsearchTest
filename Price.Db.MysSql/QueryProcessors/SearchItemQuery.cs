using System.Data.Entity;
using Db.Entities;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;

namespace Price.Db.Postgress.QueryProcessors
{
    public class SearchItemQuery : TypedQuery<SearchItemEntity, string>, ISearchItemQuery
    {
        public SearchItemQuery(DbContext db) : base(db)
        {
        }
    }
}