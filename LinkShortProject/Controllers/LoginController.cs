using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortProject.Controllers
{
    public class LoginController : Controller
    {
        UserManager<IdentityUser> userMng;
        SignInManager<IdentityUser> signInMng;
        public LoginController(UserManager<IdentityUser> userMng, SignInManager<IdentityUser> signInMng)
        {
            this.userMng = userMng;
            this.signInMng = signInMng;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                IdentityUser appUser = await userMng.FindByNameAsync(loginModel.Login);
                if (appUser != null)
                {
                    await signInMng.SignOutAsync();
                    var result = await signInMng.PasswordSignInAsync(appUser, loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Cabinet");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
           
            return Redirect("/login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OutLogin()
        {
            await signInMng.SignOutAsync();
            return Redirect("/login");
        }
    }
}