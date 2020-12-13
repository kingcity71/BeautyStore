using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllItems();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Item(Guid? id)
        {
            var category = id!=null?
                await _categoryService.GetItem(id.Value)
                : Activator.CreateInstance<CategoryModel>();
            ViewBag.Parents = await GetParents();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Item(CategoryModel model)
        {
            if (model.Id == Guid.Empty)
                model = await _categoryService.Create(model);
            else
                model = await _categoryService.Update(model);
            
            ViewBag.Parents = await GetParents();
            
            return View(model);
        }

        private async Task<IEnumerable<CategoryModel>> GetParents()
        {
            var allCategories = await _categoryService.GetAllItems();
            var parents = allCategories.Where(x => x.Parent == null).ToList();
            return parents;
        }
    }
}