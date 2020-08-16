using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Site.Identity;
using Site.Services;
using Site.ViewModels;
using Act = Services.Models.Constants.Account;

namespace Site.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRegistrationService _userRegistrationService;

        public AccountController(
            
            IUserRegistrationService userRegistrationService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _userRegistrationService = userRegistrationService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous] public ActionResult Register() => View();
        [AllowAnonymous] public ActionResult ForgotPassword() => View();
        [AllowAnonymous] public ActionResult ForgotPasswordConfirmation() => View();
        [AllowAnonymous] public ActionResult ResetPasswordConfirmation() => View();
        [AllowAnonymous] public ActionResult ResetPassword(string code) => code == null ? View("Error") : View();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", Act.LoginFailMessage);
                return View(model);
            }
            
            //if user's email is not confirmed, resend link and return view with error
            if (!user.EmailConfirmed)
            {
                await _userRegistrationService.SendEmailConfirmationLink(Request, model.Email);
                ModelState.AddModelError("", Act.UnconfirmedEmailMessage);
                return View(model);
            }

            //var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false);
            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (loginResult.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl) || returnUrl.Contains("confirmemail"))
                    return RedirectToAction("Index", "Home");
                return RedirectToLocal(returnUrl);
            }

            if (loginResult.IsLockedOut) return View("Lockout");
            if (loginResult.Succeeded == false) ModelState.AddModelError("", Act.LoginFailMessage);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var identityResult = await _userRegistrationService.CreateUser(model.Email, model.Password);
            if (identityResult.Succeeded)
            {
                await _userRegistrationService.SendEmailConfirmationLink(Request, model.Email);
                ViewBag.Message = Act.EmailConfirmationLinkSentMessage;
                return View("Info");
            }
            else
            {
                foreach (var identityResultError in identityResult.Errors)
                {
                    ModelState.AddModelError(identityResultError.Code, identityResultError.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet("Account/ConfirmEmail/{userId}")]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if(string.IsNullOrEmpty(userId)) ModelState.AddModelError(string.Empty, Act.UserIdCantBeNullError);
            if(string.IsNullOrEmpty(code)) ModelState.AddModelError(string.Empty, Act.ConfirmationCodeCantBeNullError);
            if(!ModelState.IsValid) return View();
            var confirmEmailSuccess = await _userRegistrationService.ConfirmEmail(userId, code);
            if(!confirmEmailSuccess) ModelState.AddModelError("", Act.EmailConfirmationError);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var sendEmailISuccess = await _userRegistrationService.SendPasswordResetLink(Request, model.Email);
            if (!sendEmailISuccess) ModelState.AddModelError("", Act.SendPasswordRestLinkError);
            return View("ForgotPasswordConfirmation");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest model)
        {
            if (!ModelState.IsValid) return View(model);
            var resetPasswordSuccess = await _userRegistrationService.ResetPassword(model);
            if (resetPasswordSuccess) return View("ResetPasswordConfirmation");
            ModelState.AddModelError("", Act.ResetPasswordError);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }
    }
}