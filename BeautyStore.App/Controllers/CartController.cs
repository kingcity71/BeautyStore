using BeautyStore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BeautyStore.App.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public CartController(RoleManager<IdentityRole> roleManager, ICartService cartService, IUserService userService)
            :base(roleManager)
        {
            _cartService = cartService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Payment()
            => View();

        [HttpGet]
        public async Task Pay(Guid cartId)
            => await _cartService.Pay(cartId);

        [HttpGet]
        public async Task Remove(Guid cartId)
            => await _cartService.Remove(cartId);

        [HttpGet]
        public async Task<IActionResult> Add(Guid productId)
        {
            var user = await _userService.GetUser(GetCurrentUser());
            await _cartService.Hold(productId,user.Id);
            return RedirectToAction("MyCart");
        }
        [HttpGet]
        public async Task<bool> IsPaymentPossible(Guid productId)
            => await _cartService.IsPaymentPossible(productId);

        [HttpGet]
        public async Task<IActionResult> MyCart()
        {
            var user = await _userService.GetUser(GetCurrentUser());
            var cart = await _cartService.GetUserCart(user.Id);
            return View(cart);
        }
    }
}