using Identity_MVC.Models;
using Identity_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Identity_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login");
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterVM
            {
                Roles = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(ModelState.IsValid)
            {
                var newUser = new AppUser
                {
                    UserName = registerVM.Email,
                    Email = registerVM.Email,
                    Direccion = registerVM.Address,
                    Nombres = registerVM.Nombres
                };

                var result = await _userManager.CreateAsync(newUser, registerVM.Password);
                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(registerVM.SelectedRole) && await _roleManager.RoleExistsAsync(registerVM.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(newUser, registerVM.SelectedRole);
                    }
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }                

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            var model = new RegisterVM
            {
                Roles = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
