using BeautyStore.Entities;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeautyStore.BLL.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public async Task<int> GetBalance(Guid productId)
        {
            var storeItem = await _storeRepository.GetItemByProductId(productId);
            if (storeItem == null) return 0;
            return storeItem.Count;
        }

        public async Task Supply(Guid productId, int count)
        {
            var storeItem = await _storeRepository.GetItemByProductId(productId);
            if (storeItem == null)
            {
                storeItem = new Store
                {
                    Id = Guid.NewGuid(),
                    Count = count,
                    ProductId = productId
                };
                await _storeRepository.Create(storeItem);
                return;
            }
            storeItem.Count += count;
            await _storeRepository.Update(storeItem);
        }
    }
}
