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
        public async Task CartPlus(Guid cartId, Guid productId)
            => await _cartService.CartPlus(cartId, productId);
        [HttpGet]
        public async Task CartMinus(Guid cartId, Guid productId)
            => await _cartService.CartMinus(cartId, productId);
        public async Task CartProductTrash(Guid cartId, Guid productId)
            => await _cartService.CartProductTrash(cartId, productId); 

        [HttpGet]
        public async Task<int> ProductCount(Guid productId, Guid branchId)
            => await _cartService.GetProductCount(productId, branchId);

        //[HttpGet]
        //public async Task AddToCart(Guid productId, Guid branchId, int count)
        //{
        //    var user = await _userService.GetUser(GetCurrentUser());
        //    await _cartService.Hold(productId, user.Id, branchId);
        //}

        [HttpGet]
        public async Task<IActionResult> Add(Guid productId, Guid branchId, int count)
        {
            var user = await _userService.GetUser(GetCurrentUser());
            try
            {
                await _cartService.Hold(productId, user.Id, branchId, count);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
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