using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllItems();
        Task<T> GetItem(Guid id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(Guid id);
    }
}