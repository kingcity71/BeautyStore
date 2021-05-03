using BeautyStore.App.Models;
using BeautyStore.Entities;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly IBaseRepository<Branch> _branchRepo;

        public ProductController(IProductService productService,
            ICategoryService categoryService,
            IStoreService storeService,
            IBaseRepository<Branch> branchRepo)
        {
            _productService = productService;
            _categoryService = categoryService;
            _storeService = storeService;
            _branchRepo = branchRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Item(Guid id)
        {
            var product = await _productService.GetItem(id, includeStoreInfo: true);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var product = id != null ?
                await _productService.GetItem(id.Value)
                : new ProductModel();

            ViewBag.Categories = await GetCategories();

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel model)
        {
            if (model.Id == Guid.Empty) await _productService.Create(model);
            else await _productService.Update(model);


            ViewBag.Categories = await GetCategories();
            return View(model);
        }
        private async Task<IEnumerable<CategoryModel>> GetCategories()
            => await _categoryService.GetAllItems();
        
        [HttpGet]
        public async Task<IActionResult> Supply(Guid productId)
        {
            var product = await _productService.GetItem(productId);
            var supplyModel = new SupplyModel
            {
                Product = product,
                Count = 0,
                Branchs = await _branchRepo.GetAllItems()
            };
            return View(supplyModel);
        }

        [HttpGet]
        public async Task<int> GetBalance(Guid productId, Guid branchId)
            => await _storeService.GetBalance(productId, branchId);

        [HttpPost]
        public async Task<IActionResult> Supply(SupplyModel model)
        {
            await _storeService.Supply(model.Product.Id, model.Count, model.BranchId);
            return RedirectToAction("Item", new { id = model.Product.Id });
        }
    }
}