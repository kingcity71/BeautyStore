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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<IEnumerable<Product>> GetItems(string key, Guid[] categoryIds, int take, int skip)
        {
            using var context = GetContext();
            var entities = await context
                .Products
                .AsNoTracking()
                .Where(x => categoryIds != null && categoryIds.Any() ? categoryIds.Contains(x.CategoryId) : true)
                .Where(x => !string.IsNullOrEmpty(key)? x.Title.ToLower().Contains(key.ToLower()): true)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return entities;
        }
    }
}
