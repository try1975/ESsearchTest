using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Price.Db.Entities
{
    public interface ITypedQuery<T, in TK> where T : class, IEntity<TK>
    {
        IQueryable<T> GetEntities();
        T GetEntity(TK id);
        Task<T> GetEntityAsync(TK id);
        T InsertEntity(T entity);
        bool InsertEntities(List<T> entities);
        T UpdateEntity(T entity);
        bool DeleteEntity(TK id);

        DbContext GetDbContext();
    }
}