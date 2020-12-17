using BeautyStore.App.Models;
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

        public ProductController(IProductService productService, ICategoryService categoryService, IStoreService storeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> Item(Guid id)
        {
            var product = await _productService.GetItem(id);
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
            var balance = await _storeService.GetBalance(productId);
            var product = await _productService.GetItem(productId);
            var supplyModel = new SupplyModel
            {
                Product = product,
                Count = 0,
                Balance = balance
            };
            return View(supplyModel);
        }

        [HttpPost]
        public async Task<IActionResult> Supply(SupplyModel model)
        {
            await _storeService.Supply(model.Product.Id, model.Count);
            return RedirectToAction("Item", new { id = model.Product.Id });
        }
    }
}