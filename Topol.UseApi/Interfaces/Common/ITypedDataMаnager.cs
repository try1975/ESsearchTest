using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto;

namespace Topol.UseApi.Interfaces.Common
{
    public interface ITypedDataMànager<T, K> where T : class, IDto<K>
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItem(K id);
        Task<T> PostItem(T item);
        Task<T> PutItem(T item);
        Task<bool> DeleteItem(T item);
    }
}