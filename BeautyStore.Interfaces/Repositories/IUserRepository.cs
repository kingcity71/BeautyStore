using BeautyStore.Entities;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetItem(string email);
    }
}
