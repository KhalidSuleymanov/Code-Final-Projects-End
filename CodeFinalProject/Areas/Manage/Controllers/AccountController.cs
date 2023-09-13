using CodeFinalProject.Areas.Manage.ViewModels;
using CodeFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CodeFinalProject.Areas.Manage.Controllers
{

    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));
        //    return Content("created");
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        FullName = "Super Admin 1",
        //        UserName = "SuperAdmin1",
        //    };
        //    var result = await _userManager.CreateAsync(admin, "Admin1234");
        //    await _userManager.AddToRoleAsync(admin, "SuperAdmin");
        //    return Content("yaradildi");
        //}

        public IActionResult Login(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(
            AdminLoginViewModel adminVM
          )
        {
            string returnUrl = TempData["ReturnUrl"].ToString();
            if (!ModelState.IsValid)
            {
                return View();
            }
            var admin = await _userManager.FindByNameAsync(adminVM.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }
            if (returnUrl == null)
            {
               return RedirectToAction("index", "dashboard"); 
            } 
            else {
                return Redirect(returnUrl);
            }
        }
    }
}
