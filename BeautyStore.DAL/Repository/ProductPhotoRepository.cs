using BeautyStore.Entities;
using BeautyStore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.DAL.Repository
{
    public class ProductPhotoRepository : BaseRepository<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(IConfiguration configuration) : base(configuration){}

        public async Task<IEnumerable<ProductPhoto>> GetItemsByProductId(Guid productId)
        {
            using var context = GetContext();
            return await context.ProductPhotos
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public void RemoveRange(IEnumerable<ProductPhoto> productPhotos)
        {
            using var context = GetContext();
            context.ProductPhotos.RemoveRange(productPhotos);
        }
    }
}
