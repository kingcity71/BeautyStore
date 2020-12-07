using BeautyStore.Models;
using System;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUser(UserModel userModel);
        Task<UserModel> GetUser(Guid id);
        Task<UserModel> GetUser(string email);
    }
}
