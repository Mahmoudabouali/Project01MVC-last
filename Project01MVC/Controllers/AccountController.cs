using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.PresentationLayer.Utilites;
using Mvc.PresentationLayer.ViewModels;
using NuGet.Common;
using Project01MVC.Controllers;

namespace Mvc.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
            if(!ModelState.IsValid) return View(model);

            //create application user object
            var user = new ApplicationUser
			{
                UserName = model.UserName,
                Email = model.Email,
                FisrtName = model.FirstName,
                LastName = model.LastName,
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded) 
                return RedirectToAction(nameof(Login));

            foreach (var item in result.Errors)
                ModelState.AddModelError(string.Empty, item.Description);
			return View();
		}
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
            //1. server side validation
            if (!ModelState.IsValid) return View(model);
            //2. check if user exists
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if(user is not null)
            {
                //3. check password
                if(_userManager.CheckPasswordAsync(user, model.Password).Result)
                {
                    //4. login if password is correct
                    var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if(result.Succeeded) return RedirectToAction(nameof(HomeController.Index),nameof(HomeController).Replace("Controller",string.Empty));
                }
            }
            ModelState.AddModelError(string.Empty, "incorrect email or password");
            return View(model);
		}
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
		public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            //1. ceck if user exist
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if(user is not null)
            {
                // create reset password token
                var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                // create url to reset password
                var url = Url.Action(nameof(ResetPassword), nameof(AccountController).Replace("Controller", string.Empty), new { email = model.Email, Token = token }, Request.Scheme);
                // create email object
                var email = new Email
                {
                    Subject = "reset password",
                    Body = url!,
                    Recipient = model.Email
                };
                // send email
                MailSettings.SendEmail(email);
                // redirect to check your inbox
                return RedirectToAction(nameof(CheckYourInBox));
            }
            ModelState.AddModelError(string.Empty, "user not found");
            return View(model);
        }
        public IActionResult CheckYourInBox()
        {
            return View();
        }
		public IActionResult ResetPassword(string email,string token)
		{
            if (email is null || token is null) return BadRequest();
            TempData["Email"] = email;
            TempData["Token"] = token;
			return View();
		}
        [HttpPost]
		public IActionResult ResetPassword(ResetPasswordViewModel model)
		{
            model.Token = TempData["Token"]?.ToString() ?? string.Empty;
            model.Email = TempData["Email"]?.ToString() ?? string.Empty;

			if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user != null)
            {
                var result = _userManager.ResetPasswordAsync(user, model.Token, model.Password).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            ModelState.AddModelError(string.Empty, "user not found");
			return View(model);
		}
	}
}
