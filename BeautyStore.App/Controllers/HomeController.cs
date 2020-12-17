using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeautyStore.App.Models;
using Microsoft.AspNetCore.Identity;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;

namespace BeautyStore.App.Controllers
{
    public class HomeController : BaseController
    {
        const int SKIP = 10;
        const int TAKE = 12;
        private readonly ISearchService _searchService;
        private readonly ICategoryService _categoryService;

        public HomeController(RoleManager<IdentityRole> roleManager, ISearchService searchService, ICategoryService categoryService) 
            : base(roleManager)
        {
            _searchService = searchService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid? categoryId)
        {
            var model = Activator.CreateInstance<SearchModel>();
            model.Products = await _searchService.Search(categoryId, null, int.MaxValue, SKIP * model.Page);
            model.CategoryId = categoryId;
            ViewBag.Categories = await GetCategories();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(SearchModel model)
        {
            var key = !string.IsNullOrEmpty(model.Key) ? model.Key.Trim() : string.Empty;
            model.Products = await _searchService.Search(model.CategoryId, key, int.MaxValue, SKIP * model.Page);

            ViewBag.Categories = await GetCategories();
            return View(model);
        }

        private Task<IEnumerable<CategoryModel>> GetCategories()
            => _categoryService.GetAllItems();

    }
}
