using BeautyStore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IProductPhotoRepository : IBaseRepository<ProductPhoto>
    {
        Task<IEnumerable<ProductPhoto>> GetItemsByProductId(Guid productId);
        void RemoveRange(IEnumerable<ProductPhoto> productPhotos);
    }
}
