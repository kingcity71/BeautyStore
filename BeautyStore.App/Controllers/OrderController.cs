using BeautyStore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.App.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService cartService;
        private readonly IUserService userService;

        public OrderController(ICartService cartService, IUserService userService)
        {
            this.cartService = cartService;
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var userId  = (await userService.GetUser(User.Identity.Name)).Id;
            var orders = await cartService.GetOrders(userId);
            return View(orders);
        }
    }
}
