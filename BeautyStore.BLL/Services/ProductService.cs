using BeautyStore.Entities;
using BeautyStore.Interfaces.Helpers;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepo;
        private readonly IBaseRepository<Branch> _branchRepo;
        private readonly IBaseRepository<Review> _reviewRepo;
        private readonly IProductPhotoRepository _productPhotoRepo;
        private readonly IPhotoRepository _photoRepo;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;
        private readonly IBaseRepository<ProductNotifications> _pnRepo;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<Product> productRepo,
            IProductPhotoRepository productPhotoRepo,
            IPhotoRepository photoRepo,
            ICategoryService categoryService,
            IMapper mapper,
            IStoreService storeService,
            IUserService userService,
            IBaseRepository<Branch> branchRepo, IBaseRepository<Review> reviewRepo, IBaseRepository<ProductNotifications> pnRepo = null)
        {
            _productRepo = productRepo;
            _productPhotoRepo = productPhotoRepo;
            _photoRepo = photoRepo;
            _categoryService = categoryService;
            _mapper = mapper;
            _branchRepo = branchRepo;
            _storeService = storeService;
            _reviewRepo = reviewRepo;
            _userService = userService;
            _pnRepo = pnRepo;
        }

        public async Task Subscribe(Guid userId, Guid productId)
        {
            var notiif = await _pnRepo.GetItem(x => x.ProductId == productId && x.UserId == userId);
            if (notiif != null) return;
            await _pnRepo.Create(new ProductNotifications { ProductId = productId, UserId = userId });
        }
            

        public async Task<List<ProductModel>> Subscribes(Guid userId)
        {
            var notifs = await _pnRepo.GetMany(x => x.UserId == userId);
            var result = new List<ProductModel>();
            foreach (var notif in notifs)
            {
                var prod = await GetItem(notif.ProductId);
                result.Add(prod);

            }
            return result;
        }
            
        public async Task Unsubscribe(Guid userId, Guid prodId)
        {
            var notif = await _pnRepo.GetItem(x => x.ProductId == prodId && userId == x.UserId);
            await  _pnRepo.Delete(notif.Id);
        }


        public async Task DeleteReview(Guid reviewId)
            => await _reviewRepo.Delete(reviewId);

        public async Task<ProductModel> Create(ProductModel productModel)
        {
            productModel.Id = Guid.NewGuid();
            var productEntity = _mapper.Map<ProductModel, Product>(productModel);
            productEntity.CategoryId = productModel.Category.Id;

            await _productRepo.Create(productEntity);

            foreach (var item in productModel.Photos)
            {
                var photoModel = item.Value;
                photoModel.Id = Guid.NewGuid();
                var order = item.Key;

                var photoEntity = _mapper.Map<PhotoModel, Photo>(photoModel);

                await _photoRepo.Create(photoEntity);

                await _productPhotoRepo.Create(new ProductPhoto 
                { Id = Guid.NewGuid(), PhotoId = photoEntity.Id, ProductId = productEntity.Id, DisplayOrder = order });
            }

            return productModel;
        }
        public async Task CreateReview(Guid userId, Guid productId, string comment, int stars)
        {
            var entity = new Review
            {
                Comment = comment,
                ProductId = productId,
                UserId = userId,
                Stars = stars
            };
            await _reviewRepo.Create(entity);
        }
        public async Task Delete(Guid id)
        {
            var productPhotos = await _productPhotoRepo.GetItemsByProductId(id);
            var photos = productPhotos.Select(pp => new Photo { Id = pp.PhotoId }).ToList();

            _productPhotoRepo.RemoveRange(productPhotos);
            _photoRepo.RemoveRange(photos);
            await _productRepo.Delete(id);
        }

        public async Task<ProductModel> GetItem(Guid id, bool onlyCoverPhoto = false, bool includeStoreInfo = false, bool includeReview = false)
        {
            var productEntity = await _productRepo.GetItem(id);
            var productModel = _mapper.Map<Product, ProductModel>(productEntity);

            var photos = await _photoRepo.GetPhotosByProductId(id, onlyCoverPhoto);
            
            foreach (var photo in photos)
                productModel.Photos.Add(photo.Key, _mapper.Map<Photo, PhotoModel>(photo.Value));

            productModel.Category = await _categoryService.GetItem(productEntity.CategoryId);

            var branchs = (await _branchRepo.GetAllItems())
                .Select(x => _mapper.Map<Branch, BranchModel>(x))
                .ToList();

            if (includeStoreInfo)
            {
                productModel.BranchCounts = new Dictionary<BranchModel, int>();
                foreach (var branch in branchs)
                {
                    var balance = await _storeService.GetBalance(id, branch.Id);
                    productModel.BranchCounts.Add(branch, balance);
                }
            }

            if (includeReview)
            {
                var reviews = await _reviewRepo.GetMany(x => x.ProductId == id);
                foreach(var review in reviews)
                {
                    var reviewModel = new ReviewModel
                    {
                        Id = review.Id,
                        Stars = review.Stars,
                        Comment = review.Comment,
                        User = await _userService.GetUser(review.UserId)
                    };
                    productModel.Reviews.Add(reviewModel);
                }
            }

            return productModel;
        }

        public async Task<ProductModel> Update(ProductModel productModel)
        {
            var entityProduct = _mapper.Map<ProductModel, Product>(productModel);
            entityProduct.CategoryId = productModel.Category.Id;

            await _productRepo.Update(entityProduct);

            var oldProductPhoto = await _productPhotoRepo.GetItemsByProductId(productModel.Id);
            var oldProductPhotoIds = oldProductPhoto.Select(x => x.PhotoId).ToList();
            var newProductPhotoIds = productModel.Photos.Select(x => x.Value.Id).ToList();

            var idsToDelete = oldProductPhotoIds
                .Except(newProductPhotoIds)
                .ToList();

            var idsToUpdate = oldProductPhotoIds
                .Intersect(newProductPhotoIds)
                .ToList();

            _productPhotoRepo.RemoveRange(oldProductPhoto.Where(x => idsToDelete.Contains(x.PhotoId)));

            var photosToUpdate = productModel.Photos
                .Where(x => idsToUpdate.Contains(x.Value.Id))
                .Select(x => _mapper.Map<PhotoModel, Photo>(x.Value))
                .ToList();
            foreach (var photo in photosToUpdate)
                await _photoRepo.Update(photo);
            
            foreach(var photoModel in productModel.Photos.Where(x=>x.Value.Id == Guid.Empty))
            {
                var photoId = Guid.NewGuid();
                var photo = new Photo { Id = photoId, Base64 = photoModel.Value.Base64 };
                await _photoRepo.Create(photo);
                var productPhoto = new ProductPhoto
                {
                    Id = Guid.NewGuid(),
                    DisplayOrder = photoModel.Key,
                    PhotoId = photoId,
                    ProductId = productModel.Id
                };
                await _productPhotoRepo.Create(productPhoto);
            }

            return productModel;
        }
    }
}
