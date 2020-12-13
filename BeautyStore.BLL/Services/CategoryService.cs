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
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(IBaseRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<CategoryModel> Create(CategoryModel categoryModel)
        {
            categoryModel.Id = Guid.NewGuid();
            var entity = _mapper.Map<CategoryModel, Category>(categoryModel);
            var parentId = categoryModel.Parent?.Id;
            entity.ParentCategoryId = parentId != null && parentId == Guid.Empty ? null : parentId;
            await _categoryRepo.Create(entity);
            return categoryModel;
        }
        public async Task<IEnumerable<CategoryModel>> GetAllItems()
        {
            var allEntities = await _categoryRepo.GetAllItems();
            var allModels = allEntities.Select(entity => _mapper.Map<Category, CategoryModel>(entity)).ToList();

            foreach (var model in allModels)
            {
                var parentId = allEntities.FirstOrDefault(x => x.Id == model.Id)?.ParentCategoryId;
                if (parentId != null)
                    model.Parent = allModels.FirstOrDefault(m => m.Id == parentId.Value);
            }
                

            return allModels;
        }

        public async Task<CategoryModel> GetItem(Guid id)
        {
            var categoryEntity = await _categoryRepo.GetItem(id);
            var categoryModel = _mapper.Map<Category, CategoryModel>(categoryEntity);

            if (categoryEntity.ParentCategoryId.HasValue)
            {
                var categoryParentEntity = await _categoryRepo.GetItem(categoryEntity.ParentCategoryId.Value);
                var categoryParentModel = _mapper.Map<Category, CategoryModel>(categoryParentEntity);
                categoryModel.Parent = categoryParentModel;
            }

            return categoryModel;
        }

        public async Task<CategoryModel> Update(CategoryModel categoryModel)
        {
            var entity = _mapper.Map<CategoryModel, Category>(categoryModel);
            entity.ParentCategoryId = categoryModel.Parent?.Id;
            await _categoryRepo.Update(entity);
            return categoryModel;
        }
    }
}
