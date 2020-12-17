using BeautyStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<ProductModel>> Search(Guid? categoryId, string key, int take, int skip);
    }
}
