using BeautyStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartModel>> GetUserCart(Guid userId);
        Task Hold(Guid productiId, Guid userId);
        Task Remove(Guid cartId);
        Task Pay(Guid id);
        Task<bool> IsPaymentPossible(Guid productId);
    }
}
