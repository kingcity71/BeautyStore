using BeautyStore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
       Task<IEnumerable<Product>> GetItems(string key, Guid[] categoryIds, int take, int skip);
    }
}
