using Catalog.Models;
using CatalogLib.DataBase;
using CatalogLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
       
        public AccountController()
        {
           
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userStore = new UserStore<ApplicationUser>(new DataContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = await userManager.FindAsync(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с такой комбинацией пароля и логина не существует");
                return View(model);
            }
            var authManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = model.RememberMe }, userIdentity);

            return RedirectToLocal(returnUrl);
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("index","home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userStore = new UserStore<ApplicationUser>(new DataContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
        var user = await  userManager.FindAsync(model.Email, model.Password);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь существует");
                return View(model);
            }

            user = new ApplicationUser { UserName = model.Email, Email = model.Email };
           await userManager.CreateAsync(user, model.Password);
            return RedirectToAction("Index", "Home");
        }
       

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }

}