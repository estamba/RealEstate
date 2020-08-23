using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.ExternalServices;
using RealEstate.Core.Interfaces.Services.Agents;
using RealEstate.Core.Interfaces.Services.Regions;
using RealEstate.MVC.Extensions;
using RealEstate.MVC.Models;
using RealEstate.MVC.Services;

namespace RealEstate.MVC.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly IAddAgentService addAgentService;
        private readonly IAgentService agentService;
        private readonly ILocationService locationService;
        private readonly IMapper mapper;
        private readonly IUpdateAgentService updateAgentService;
        private readonly IEmailSender emailSender;
        private readonly AccountVmService accountVmService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAddAgentService addAgentService, IAgentService agentService, ILocationService locationService, IMapper mapper,IUpdateAgentService updateAgentService, IEmailSender emailSender, AccountVmService accountVmService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.addAgentService = addAgentService;
            this.agentService = agentService;
            this.locationService = locationService;
            this.mapper = mapper;
            this.updateAgentService = updateAgentService;
            this.emailSender = emailSender;
            this.accountVmService = accountVmService;
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
            if (loginResult.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToRoute("dashboard");
                return RedirectToLocal(returnUrl);

            }
            ModelState.AddModelError("", "Invalid login credentials");
            return View(model);
        }
       
        public IActionResult SignUp()
        {
            return View();
        }
        [Route("social-login-callback")]
        public async Task<IActionResult> SocialLoginCallback(string returnUrl)
        {
            var data = await signInManager.GetExternalLoginInfoAsync();
            var result = await Request.HttpContext.AuthenticateAsync();
            var appUser = await userManager.FindByEmailAsync(data.Principal.FindFirstValue(ClaimTypes.Email));
            if (appUser is null)
            {
                SignUpVm signUpVm = new SignUpVm()
                {
                    EmailAddress = data.Principal.FindFirstValue(ClaimTypes.Email),
                    Name = data.Principal.FindFirstValue(ClaimTypes.Name)
                };
                return View("SignUp", signUpVm);
            }
            await signInManager.SignInAsync(appUser, true);
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToRoute("dashboard");
            return RedirectToLocal(returnUrl);


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
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.Now
                };
                addAgentService.Add(agent);
                await signInManager.SignInAsync(user, isPersistent: false);

             
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View();
        }
        [Route("signin/{provider}")]
        public IActionResult SignIn(string provider, string returnUrl = null)
        {

            var route = Url.Action("SocialLoginCallback", "Account");
            route = $"{route}?returnUrl={returnUrl}";
            var props = signInManager.ConfigureExternalAuthenticationProperties(provider, route);
            return Challenge(props, provider);

        }
        [HttpGet]
        public  IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var appUser = await userManager.FindByEmailAsync(model.EmailAddress);
            if(appUser is null)
            {
                ModelState.AddModelError("", "email addresss does not exist in our systems");
                return View(model);
            }
            
            var token = await userManager.GenerateUserTokenAsync(appUser, TokenOptions.DefaultProvider, "PasswordRecovery");

            var emailModel = accountVmService.GetPasswordRecoverEmailModel(appUser.Email, GetPasswordRecoveryUrl(token, appUser.Email));
            await emailSender.SendAsync(emailModel);
            return View();
        }
   
        public async Task<IActionResult> RecoverPassword(string code, string emailAddress)
        {
            var appUser = await userManager.FindByEmailAsync(emailAddress);
            if (appUser is null) return RedirectToAction("Login");
            
            var isValid = await userManager.VerifyUserTokenAsync(appUser, TokenOptions.DefaultProvider, "PasswordRecovery", code);
            if(!isValid) RedirectToAction("Login");
            return View(new PasswordRecoveryVM() { Code = code, EmailAddress = emailAddress });
        }
        [HttpPost]
        public  async Task<IActionResult> RecoverPasswordPost(PasswordRecoveryVM model)
        {
            if (!ModelState.IsValid) return View("RecoverPassword", model);
            var appUser = await userManager.FindByEmailAsync(model.EmailAddress);
            if (appUser is null) return RedirectToAction("Login");

            var isValid = await userManager.VerifyUserTokenAsync(appUser, TokenOptions.DefaultProvider, "PasswordRecovery", model.Code);
            if (isValid)
            {
                appUser.PasswordHash = userManager.PasswordHasher.HashPassword(appUser, model.Password);
               await  userManager.UpdateAsync(appUser);
            }
           return RedirectToAction("Login");
        
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var userId = userManager.GetUserId(User);
            var agent = await agentService.GetAgentByApplicationUserIdAsync(userId);
            UpdateProfileVM vm = new UpdateProfileVM()
            {
                City = agent?.City?.Name,
                Email = agent.ApplicationUser.Email,
                Name = agent.ApplicationUser.Name,
                PhoneNumber = agent.ApplicationUser.PhoneNumber,
                ProfilePhotoId = agent.ImageId ?? default(Guid),
                Cities = locationService.GetCities().Select(c=>c.Name).ToList(),
                AgentId = agent.Id
            };
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM model, IFormFile photo)
        {
            if (!IsFileValid(photo)) ModelState.AddModelError("", "Invalid Image Type");
            if (!IsLocationValid(model?.City)) ModelState.AddModelError("", "Invalid location");

            if (!ModelState.IsValid) return View("UpdateProfile", model);
            var updateModel = mapper.Map<UpdateProfileModel>(model);
            updateModel.ProfilePhoto = await GetDocument(photo);

            var appUser = await userManager.GetUserAsync(User);
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.Name = model.Name;

            var updatedAgent = await updateAgentService.UpdateProfile(updateModel);
            await userManager.UpdateAsync(appUser);
            model.ProfilePhotoId = (updatedAgent.ImageId is null) ? default(Guid) : updatedAgent.ImageId.Value;
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var appUser = await userManager.GetUserAsync(User);
            var result = await userManager.ChangePasswordAsync(appUser, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "error updating password");
                return View(model);
            }
            return RedirectToRoute("dashboard");
        }
        private string GetPasswordRecoveryUrl(string token, string emailAddress)
        {
            string route = Url.Action("RecoverPassword", "Account", new { code = token,emailAddress });

            return $"{Request.Scheme}://{Request.Host}{route}";
        }
        private bool IsFileValid(IFormFile file)
        {
            if (file is null) return true;
            
            return file.ContentType.Contains("image");
        }
        private async Task<Document> GetDocument(IFormFile file)
        {
            if (file is null) return default;
            var bytes = await file.ToByteArrayAsync();
            Document document = new Document()
            {
                Id = Guid.NewGuid(),
                ContentType = file.ContentType,
                Extension = Path.GetExtension(file.FileName),
                Name = file.FileName,
                Content = bytes
            };
            return document;
        }
        private bool IsLocationValid(string city)
        {
            if (string.IsNullOrEmpty(city)) return true;

            return locationService.GetCities().Any(c => c.Name.ToLower() == city.ToLower());
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