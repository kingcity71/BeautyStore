using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllItems();
        Task<T> GetItem(Guid id);
        Task<T> GetItem(Expression<Func<T, bool>> expression);
        Task<T> Create(T item);
        Task Update(T item);
        Task Delete(Guid id);
    }
}