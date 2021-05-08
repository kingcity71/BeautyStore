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
        private readonly IProductService _productService;
        private readonly IBaseRepository<CartProduct> _cartProductRepo;

        private readonly IBaseRepository<Branch> _branchRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IStoreRepository storeRepository, 
            IProductService productService, 
            IBaseRepository<CartProduct> cartProductRepo,
            IBaseRepository<Branch> branchRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _storeRepository = storeRepository;
            _productService = productService;
            _cartProductRepo = cartProductRepo;
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        

        public async Task<Dictionary<string, int>> ChechAvailable(Guid cartId)
        {
            var result = new Dictionary<string, int>();
            var cart = await _cartRepository.GetItem(x => x.Id == cartId);
            var cartRepo = await _cartProductRepo.GetMany(x => x.CartId == cartId);

            foreach (var item in cartRepo)
            {
                var store = await _storeRepository.GetItem(x => x.BranchId == cart.BranchId && x.ProductId == item.ProductId);
                if (store.Count < item.Count)
                    result.Add((await _productService.GetItem(item.ProductId)).Title, store.Count);
            }

            return result;
        }

        public async Task CartProductTrash(Guid cartId, Guid productId)
        {
            var item = await _cartProductRepo.GetItem(x => x.ProductId == productId && x.CartId == cartId);
            await _cartProductRepo.Delete(item.Id);
        }
        public async Task CartPlus(Guid cartId, Guid productId)
        {
            var item = await _cartProductRepo.GetItem(x => x.ProductId == productId && x.CartId == cartId);
            item.Count++;
            await _cartProductRepo.Update(item);
        }
        public async Task CartMinus(Guid cartId, Guid productId)
        {
            var item = await _cartProductRepo.GetItem(x => x.ProductId == productId && x.CartId == cartId);
            item.Count--;
            await _cartProductRepo.Update(item);
        }
        public async Task<IEnumerable<CartModel>> GetUserCart(Guid userId)
        {
            var carts = await _cartRepository.GetMany(x=>x.UserId == userId && x.Status != Entities.Enum.BasketStatus.Paid);
            var cartProducts = carts != null ? await _cartProductRepo.GetMany(x => carts.Select(x => x.Id).ToList().Contains(x.CartId)) : null;
            var models = Activator.CreateInstance<List<CartModel>>();
            
            foreach(var cart in carts)
            {
                var cartModel = new CartModel();
                var branch = await _branchRepository.GetItem(x => x.Id == cart.BranchId);
                
                cartModel.BranchId = branch.Id;
                cartModel.BranchTitle = $"{branch.Name} ({branch.Address})";
                cartModel.Id = cart.Id;
                cartModel.Status = (BasketStatus)((int)cart.Status);
                cartModel.ProductCounts = new Dictionary<ProductModel, int>();
                foreach(var cartProduct in cartProducts.Where(x=>x.CartId == cart.Id))
                {
                    var productModel = await _productService.GetItem(cartProduct.ProductId);
                    cartModel.ProductCounts.Add(productModel, cartProduct.Count);
                }
                models.Add(cartModel);
            }
            
            return models;
        }

        public async Task Hold(Guid productiId, Guid userId, Guid branchId, int count)
        {
            var cart = await _cartRepository.GetItem(x => x.Status != Entities.Enum.BasketStatus.Paid && x.BranchId == branchId);
            if(cart == null)
            {
                cart = new Cart
                {
                    BranchId = branchId,
                    Status = Entities.Enum.BasketStatus.Hold,
                    UserId = userId
                };
                cart = await _cartRepository.Create(cart);
            }

            var cartProduct = await _cartProductRepo.GetItem(x => x.CartId == cart.Id && x.ProductId == productiId);

            if(cartProduct == null)
            {
                cartProduct = new CartProduct
                {
                    CartId = cart.Id,
                    Count = count,
                    ProductId = productiId
                };
                cartProduct = await _cartProductRepo.Create(cartProduct);
            }
            else
            {
                cartProduct.Count += count;
                await _cartProductRepo.Update(cartProduct);
            }
        }

        public async Task Remove(Guid cartId)
            =>  await _cartRepository.Delete(cartId);

        public async Task<bool> IsPaymentPossible(Guid productId)
            => await _storeRepository.IsCountMoreThanOne(productId);

        public async Task Pay(Guid id)
        {
            var entity = await _cartRepository.GetItem(id);
            entity.Status = Entities.Enum.BasketStatus.Paid;
            await _cartRepository.Update(entity);

            var cartProducts = await _cartProductRepo.GetMany(x => x.CartId == id);
            foreach(var prod in cartProducts)
            {
                var storeEntity = await _storeRepository.GetItemByProductId(prod.ProductId);
                storeEntity.Count -= prod.Count;
                await _storeRepository.Update(storeEntity);
            }
        }

        public async Task<int> GetProductCount(Guid productId, Guid branchId)
        {
            var storeItem = await _storeRepository.GetItem(x => x.ProductId == productId && x.BranchId == branchId);
            return storeItem != null ? storeItem.Count : 0;
        }

        public async Task<IEnumerable<CartModel>> GetOrders(Guid userId)
        {
            var carts = await _cartRepository.GetMany(x => x.UserId == userId && x.Status == Entities.Enum.BasketStatus.Paid);
            var cartProducts = carts != null ? await _cartProductRepo.GetMany(x => carts.Select(x => x.Id).ToList().Contains(x.CartId)) : null;
            var models = Activator.CreateInstance<List<CartModel>>();

            foreach (var cart in carts)
            {
                var cartModel = new CartModel();
                var branch = await _branchRepository.GetItem(x => x.Id == cart.BranchId);

                cartModel.BranchId = branch.Id;
                cartModel.BranchTitle = $"{branch.Name} ({branch.Address})";
                cartModel.Id = cart.Id;
                cartModel.Status = (BasketStatus)((int)cart.Status);
                cartModel.ProductCounts = new Dictionary<ProductModel, int>();
                foreach (var cartProduct in cartProducts.Where(x => x.CartId == cart.Id))
                {
                    var productModel = await _productService.GetItem(cartProduct.ProductId);
                    cartModel.ProductCounts.Add(productModel, cartProduct.Count);
                }
                models.Add(cartModel);
            }

            return models;
        }
    }
}
