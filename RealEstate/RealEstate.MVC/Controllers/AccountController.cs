using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Services.Agents;
using RealEstate.MVC.Models;

namespace RealEstate.MVC.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly IAddAgentService addAgentService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAddAgentService addAgentService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.addAgentService = addAgentService;
        }
        public IActionResult Index()
        {
            return View();
            
        }
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);


            var loginResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password,true, lockoutOnFailure: false);
            if (loginResult.Succeeded) return RedirectToLocal(returnUrl);
            ModelState.AddModelError("", "Invalid login credentials");
            return View(model);
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpVm model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new ApplicationUser { UserName = model.EmailAddress, Email = model.EmailAddress, PhoneNumber = model.PhoneNumber, NormalizedUserName = model.Name, Name =model.Name };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                Agent agent = new Agent()
                {
                    ApplicationUserId = user.Id,
                    Id = Guid.NewGuid()
                };
                addAgentService.Add(agent);
                await signInManager.SignInAsync(user, isPersistent: false);


                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View();
        }
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
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
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}