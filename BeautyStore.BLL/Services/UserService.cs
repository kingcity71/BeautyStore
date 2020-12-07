using BeautyStore.Entities;
using BeautyStore.Interfaces.Helpers;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using System;
using System.Threading.Tasks;

namespace BeautyStore.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task CreateUser(UserModel userModel)
        {
            var entity = _mapper.Map<UserModel, User>(userModel);
            await _userRepo.Create(entity);
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            var entity = await _userRepo.GetItem(id);
            var model = _mapper.Map<User, UserModel>(entity);
            return model;
        }

        public async Task<UserModel> GetUser(string email)
        {
            var entity = await _userRepo.GetItem(email);
            var model = _mapper.Map<User, UserModel>(entity);
            return model;
        }
    }
}
