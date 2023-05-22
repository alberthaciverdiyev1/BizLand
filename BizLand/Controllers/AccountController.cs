using BizLand.Models;
using BizLand.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BizLand.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]


        public async Task<IActionResult> Register(RegisterVM user)
        {
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("","erroro");
            }
            AppUser appUser = new AppUser
            {
                Name = user.Name,
                Email = user.Email,
                Surname = user.Surname,
                UserName = user.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            if (result.Succeeded) { 


                foreach(IdentityError item in  result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
          
            return RedirectToAction("Index", "Home");
        }
    }
}
