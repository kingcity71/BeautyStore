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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration){}

        public async Task<IEnumerable<Category>> GetChildrenCategories(Guid id)
        {
            using var context = GetContext();
            var entities = await context
                .Categories
                .AsNoTracking()
                .Where(x => x.ParentCategoryId.HasValue && x.ParentCategoryId.Value == id)
                .ToListAsync();
            return entities;
        }
    }
}
