﻿using BeautyStore.Entities;
using BeautyStore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.DAL.Repository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Store> GetItemByProductId(Guid productId)
        {
            using var context = GetContext();
            return await context.Store.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }
}
