using System.Data.Entity;
using Db.Entities;
using FindCompany.Db.Entities.Entities;
using FindCompany.Db.Entities.QueryProcessors;

namespace FindCompany.Db.Postgress.QueryProcessors
{
    public class FindCompanyQuery : TypedQuery<FindCompanyEntity, int>, IFindCompanyQuery
    {
        public FindCompanyQuery(FindCompanyContext db) : base(db)
        {
        }
    }
}