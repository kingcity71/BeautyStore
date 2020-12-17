using System;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface IStoreService
    {
        Task<int> GetBalance(Guid productId);
        Task Supply(Guid productId, int count);
    }
}
