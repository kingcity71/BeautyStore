using BeautyStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryModel> Create(CategoryModel categoryModel);
        Task<CategoryModel> Update(CategoryModel categoryModel);
        Task<CategoryModel> GetItem(Guid id);
        Task<IEnumerable<CategoryModel>> GetAllItems();
    }
}
