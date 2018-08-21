using Db.Entities;
using FindCompany.Db.Entities.Entities;

namespace FindCompany.Db.Entities.QueryProcessors
{
    public interface IFindCompanyQuery : ITypedQuery<FindCompanyEntity, int>
    {
        
    }
}