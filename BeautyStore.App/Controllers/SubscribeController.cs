using BeautyStore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.App.Controllers.Subscribe
{
    public class SubscribeController : Controller
    {
        private readonly IProductService productService;
        private readonly IUserService userService;

        public SubscribeController(IProductService productService, IUserService userService)
        {
            this.productService = productService;
            this.userService = userService;
        }
        
        public async Task<IActionResult> Index()
        {
            var userId = (await userService.GetUser(User.Identity.Name)).Id;
            var subscribes = await productService.Subscribes(userId);
            return View(subscribes);
        }

        public async Task Subscribe(Guid productId)
        {
            var userId = (await userService.GetUser(User.Identity.Name)).Id;
            await productService.Subscribe(userId, productId);
        }
        public async Task Unsubscribe(Guid productId)
        {
            var userId = (await userService.GetUser(User.Identity.Name)).Id;
            await productService.Unsubscribe(userId, productId);
        }
    }
}
