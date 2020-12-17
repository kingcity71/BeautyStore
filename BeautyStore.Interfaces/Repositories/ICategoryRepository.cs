using BeautyStore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetChildrenCategories(Guid id);
    }
}
