using BeautyStore.Entities;
using BeautyStore.Interfaces.Helpers;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepo;
        private readonly IProductPhotoRepository _productPhotoRepo;
        private readonly IPhotoRepository _photoRepo;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<Product> productRepo,
            IProductPhotoRepository productPhotoRepo,
            IPhotoRepository photoRepo,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _productPhotoRepo = productPhotoRepo;
            _photoRepo = photoRepo;
            _categoryService = categoryService;
            _mapper = mapper;
        }
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

                await _productPhotoRepo.Create(new ProductPhoto { Id = Guid.NewGuid(), PhotoId = photoEntity.Id, ProductId = productEntity.Id, DisplayOrder = order });
            }

            return productModel;
        }

        public async Task Delete(Guid id)
        {
            var productPhotos = await _productPhotoRepo.GetItemsByProductId(id);
            var photos = productPhotos.Select(pp => new Photo { Id = pp.PhotoId }).ToList();

            _productPhotoRepo.RemoveRange(productPhotos);
            _photoRepo.RemoveRange(photos);
            await _productRepo.Delete(id);
        }

        public async Task<ProductModel> GetItem(Guid id)
        {
            var productEntity = await _productRepo.GetItem(id);
            var productModel = _mapper.Map<Product, ProductModel>(productEntity);

            var photos = await _photoRepo.GetPhotosByProductId(id);
            foreach (var photo in photos)
                productModel.Photos.Add(photo.Key, _mapper.Map<Photo, PhotoModel>(photo.Value));

            productModel.Category = await _categoryService.GetItem(productEntity.CategoryId);

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
