using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ATV2_SENAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATV2_SENAC.DataContext;
using ATV2_SENAC.ViewModels;

namespace ATV2_SENAC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Data _contexto;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsuarioController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                Data contexto)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _contexto = contexto;

        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterUser User)
        {
            if (ModelState.IsValid) 
            {
                var user = new IdentityUser { UserName = User.Login, Email = User.Nome};
                var result = await userManager.CreateAsync(user, User.Password);


                if (result.Succeeded) 
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "ViagemItens");
                }
                foreach(var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
                return View(User);
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Login, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "ViagemItens");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "ViagemItens");
        }


    }
}
