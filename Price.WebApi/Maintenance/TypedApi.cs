using System.Collections.Generic;
using AutoMapper;
using Common.Dto;
using Db.Entities;
using log4net;
using Price.Db.Entities;

namespace Price.WebApi.Maintenance
{
    public abstract class TypedApi<TV, TD, TK> : ITypedApi<TV, TK> where TD : class, IEntity<TK> where TV : class, IDto<TK>
    {
        protected readonly ITypedQuery<TD, TK> Query;
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected TypedApi(ITypedQuery<TD, TK> query)
        {
            Query = query;
        }

        public virtual IEnumerable<TV> GetItems()
        {
            var list = Query.GetEntities();
            return Mapper.Map<List<TV>>(list);
        }

        public virtual TV GetItem(TK id)
        {
            return Mapper.Map<TV>(Query.GetEntity(id));
        }

        public virtual TV AddItem(TV dto)
        {
            var entity = Mapper.Map<TD>(dto);
            return Mapper.Map<TV>(Query.InsertEntity(entity));
        }

        public bool AddItems(List<TV> dtos)
        {
            var entities = Mapper.Map<List<TD>>(dtos);
            Query.InsertEntities(entities);
            return true;
        }

        public virtual TV ChangeItem(TV dto)
        {
            var entity = Mapper.Map<TD>(dto);
            return Mapper.Map<TV>(Query.UpdateEntity(entity));
        }

        public virtual bool RemoveItem(TK id)
        {
            return Query.DeleteEntity(id);
        }
    }
}