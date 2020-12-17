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
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Cart>> GetItemsByUserId(Guid userId)
        {
            using var context = GetContext();
            var carts = await context.Carts.AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return carts;
        }
    }
}
