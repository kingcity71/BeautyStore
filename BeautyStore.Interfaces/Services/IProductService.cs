using BeautyStore.Models;
using System;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductModel> GetItem(Guid id, bool onlyCoverPhoto = false, bool includeStoreInfo = false);
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Update(ProductModel productModel);
        Task Delete(Guid id);
    }
}
