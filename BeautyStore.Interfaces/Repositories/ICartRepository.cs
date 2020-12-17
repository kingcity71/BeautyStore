using BeautyStore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetItemsByUserId(Guid userId);
    }
}
