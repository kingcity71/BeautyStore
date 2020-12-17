using BeautyStore.Entities;
using BeautyStore.Interfaces.Helpers;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using BeautyStore.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IStoreRepository storeRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartModel>> GetUserCart(Guid userId)
        {
            var entites = await _cartRepository.GetItemsByUserId(userId);
            var models = entites.Select(entity => _mapper.Map<Cart, CartModel>(entity)).ToList();
            return models;
        }

        public async Task Hold(Guid productiId, Guid userId)
        {
            var model = new CartModel()
            {
                Id = Guid.NewGuid(),
                ProductId = productiId,
                UserId = userId,
                Status = BasketStatus.Hold
            };
            var entity = _mapper.Map<CartModel, Cart>(model);
            await _cartRepository.Create(entity);
        }

        public async Task Remove(Guid cartId)
            =>  await _cartRepository.Delete(cartId);

        public async Task<bool> IsPaymentPossible(Guid productId)
        {
            var storeEntity = await _storeRepository.GetItemByProductId(productId);
            if (storeEntity == null)
            {
                storeEntity = new Store
                {
                    Id = Guid.NewGuid(),
                    Count = 0,
                    ProductId = productId
                };
                await _storeRepository.Create(storeEntity);
                return false;
            }
            return storeEntity.Count > 0; 
        }

        public async Task Pay(Guid id)
        {
            var entity = await _cartRepository.GetItem(id);
            entity.Status = Entities.Enum.BasketStatus.Paid;
            await _cartRepository.Update(entity);

            var storeEntity = await _storeRepository.GetItemByProductId(id);
            storeEntity.Count -= 1;
            await _storeRepository.Update(storeEntity);
        }
    }
}
