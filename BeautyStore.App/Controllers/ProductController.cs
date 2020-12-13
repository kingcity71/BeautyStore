using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeautyStore.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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
    }
}