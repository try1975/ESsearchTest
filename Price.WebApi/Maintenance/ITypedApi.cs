using System.Collections.Generic;
using Common.Dto;

namespace Price.WebApi.Maintenance
{
    public interface ITypedApi<T, in TK> where T : class, IDto<TK>
    {
        IEnumerable<T> GetItems();
        T GetItem(TK id);
        T AddItem(T dto);
        bool AddItems(List<T> dtos);
        T ChangeItem(T dto);
        bool RemoveItem(TK id);
    }
}