using BeautyStore.Entities;
using BeautyStore.Helpers;
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
        private readonly IBaseRepository<ProductNotifications> _pnRepo;
        private readonly IBaseRepository<Branch> _brRepo;
        private readonly IBaseRepository<User> _userRepo;
        private readonly IBaseRepository<Product> _prRepo;
        private readonly IMailHelper _mail;

        public StoreService(IStoreRepository storeRepository, IBaseRepository<ProductNotifications> pnRepo = null, IMailHelper mail = null, IBaseRepository<Branch> brRepo = null, IBaseRepository<User> userRepo = null, IBaseRepository<Product> prRepo = null)
        {
            _storeRepository = storeRepository;
            _pnRepo = pnRepo;
            _mail = mail;
            _brRepo = brRepo;
            _userRepo = userRepo;
            _prRepo = prRepo;
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
            }
            else
            {
                storeItem.Count += count;
                await _storeRepository.Update(storeItem);
            }

            var branch = await _brRepo.GetItem(x => x.Id == branchId);
            var product = await _prRepo.GetItem(x => x.Id == productId);
            var notifs = await _pnRepo.GetMany(x => x.ProductId == productId);
            foreach(var notif in notifs)
            {
                var user = await _userRepo.GetItem(x => x.Id == notif.UserId);

                var message = $"В {branch.Name} ({branch.Address}) поступление товара {product.Title}";

                _mail.Send(user.Email, "Поступление товара", message);
            }
        }
    }
}
