using BeautyStore.Entities;
using BeautyStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartModel>> GetOrders(Guid userId);
        Task CartProductTrash(Guid cartId, Guid productId);
        Task CartMinus(Guid cartId, Guid productId);
        Task CartPlus(Guid cartId, Guid productId);
        Task<Dictionary<string, int>> ChechAvailable(Guid cartId);
        Task<int> GetProductCount(Guid productId, Guid branchId);
        Task<IEnumerable<CartModel>> GetUserCart(Guid userId);
        Task Hold(Guid productiId, Guid userId, Guid branchId, int count);
        Task Remove(Guid cartId);
        Task Pay(Guid id);
        Task<bool> IsPaymentPossible(Guid productId);
    }
}
