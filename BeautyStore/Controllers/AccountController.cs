using BeautyStore.App.Models;
using BeautyStore.Identity;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
            => View(new LoginViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var registerModel = Activator.CreateInstance<RegisterViewModel>();
            return View(registerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Activator.CreateInstance<User>();
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.UserName = model.Email;

                // Работа с Identity
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                user = await _userManager.FindByNameAsync(model.Email);
                // добавляем роль
                await _userManager.AddToRoleAsync(user, "User");

                // Работа с данными приложения
                var userModel = Activator.CreateInstance<UserModel>();
                userModel.FullName = model.FullName;
                userModel.Email = model.Email;
                userModel.Phone = model.Phone;
                await _userService.CreateUser(userModel);


                //авторизация
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}