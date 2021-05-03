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
        public async Task<int> GetBalance(Guid productId, Guid branchId)
        {
            var storeItem = await _storeRepository.GetItem(x=>x.ProductId == productId && x.BranchId == branchId);
            if (storeItem == null) return 0;
            return storeItem.Count;
        }

        public async Task Supply(Guid productId, int count, Guid branchId)
        {
            var storeItem = await _storeRepository.GetItem(x => x.ProductId == productId && x.BranchId == branchId);
            if (storeItem == null)
            {
                storeItem = new Store
                {
                    Id = Guid.NewGuid(),
                    Count = count,
                    ProductId = productId,
                    BranchId = branchId
                };
                await _storeRepository.Create(storeItem);
                return;
            }
            storeItem.Count += count;
            await _storeRepository.Update(storeItem);
        }
    }
}
