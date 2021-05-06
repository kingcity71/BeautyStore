using BeautyStore.Entities.Abstract;
using BeautyStore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BeautyStore.DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : Entity
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected Context GetContext() => new Context(_configuration);
        public async Task<T> Create(T item)
        {
            using var ctx = GetContext();
            var entity = await ctx.AddAsync(item);
            await ctx.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task Delete(Guid id)
        {
            using var ctx = GetContext();
            var entity = await ctx.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            ctx.Remove<T>(entity);

            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllItems()
        {
            using var ctx = GetContext();
            return await ctx.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetMany(Expression<Func<T,bool>> expression)
        {
            using var ctx = GetContext();
            return await ctx.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetItem(Guid id)
        {
            using var ctx = GetContext();
            return await ctx.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetItem(Expression<Func<T, bool>> expression)
        {
            using var ctx = GetContext();
            return await ctx.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }

        public async Task Update(T item)
        {
            using var ctx = GetContext();
            ctx.Set<T>().Update(item);
            await ctx.SaveChangesAsync();
        }
    }
}
