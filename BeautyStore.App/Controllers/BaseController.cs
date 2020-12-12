﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeautyStore.App.Controllers
{
    public class BaseController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        public BaseController(RoleManager<IdentityRole> roleManager)
            =>_roleManager = roleManager;

        protected string GetCurrentUser()
            => User.Identity.Name;

        protected string GetUserRole()
        {
            var roles = _roleManager.Roles;
            foreach (var role in roles)
                if (User.IsInRole(role.Name))
                    return role.Name;
            return null;
        }
    }
}
