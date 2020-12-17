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
    public class SearchService : ISearchService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductService _productService;

        public SearchService(IMapper mapper,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductService productService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productService = productService;
        }
        public async Task<IEnumerable<ProductModel>> Search(Guid? categoryId, string key, int take, int skip)
        {
            var categoryIds = Activator.CreateInstance <List<Guid>>();
            if (categoryId != null)
            {
                categoryIds.Add(categoryId.Value);
                categoryIds.AddRange((await _categoryRepository.GetChildrenCategories(categoryId.Value)).Select(x => x.Id).ToArray());
            }
                
            var productsEntites = await _productRepository.GetItems(key, categoryIds.ToArray(), take, skip);

            var products = Activator.CreateInstance<List<ProductModel>>();
            foreach (var id in productsEntites.Select(x => x.Id))
            {
                var model = await _productService.GetItem(id, true);
                products.Add(model);
            }

            return products;
        }
    }
}
