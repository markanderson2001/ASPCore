using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;
    
        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;

        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnURL)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.UserName,
                                                                       vm.Password,
                                                                       true, false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnURL))
                    {

                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnURL);//to where they attempted to go
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password Incorrect");
                }
            }
            return View();

        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
    }
}
