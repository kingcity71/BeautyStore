using System;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface IStoreService
    {
        Task<int> GetBalance(Guid productId, Guid branchId);
        Task Supply(Guid productId, int count, Guid branchId);
    }
}
