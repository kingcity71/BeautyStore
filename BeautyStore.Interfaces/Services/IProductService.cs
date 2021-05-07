using BeautyStore.Models;
using System;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface IProductService
    {
        Task DeleteReview(Guid reviewId);
        Task CreateReview(Guid userId, Guid productId, string comment, int stars);
        Task<ProductModel> GetItem(Guid id, bool onlyCoverPhoto = false, bool includeStoreInfo = false, bool includeReview = false);
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Update(ProductModel productModel);
        Task Delete(Guid id);
    }
}
