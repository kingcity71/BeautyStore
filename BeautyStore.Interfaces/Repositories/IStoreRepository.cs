using BeautyStore.Entities;
using System;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        Task<Store> GetItemByProductId(Guid productId);
        Task<bool> IsCountMoreThanOne(Guid productId);
    }
}
